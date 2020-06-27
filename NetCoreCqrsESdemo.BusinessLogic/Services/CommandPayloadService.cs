using MediatR;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NetCoreCqrsESdemo.BusinessLogic.Services
{
    public class CommandPayloadService
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;
        private readonly CommandService _commandService;
        
        public CommandPayloadService(IMediator mediator, ApplicationDbContext dbContext, CommandService commandService)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _commandService = commandService;
        }

        public async Task<IEnumerable<CommandPayload<T>>> ParseAndSendPayload<T>(IEnumerable<CommandPayload<T>> requests) where T : BaseDto
        {
            var resultPayloads = new List<CommandPayload<T>>();

            foreach (var request in requests)
            {
                var innerCommandType = _commandService.GetCommandByEnum(request.CommandType);
                var innerRequest = (BaseCommand<T>)Activator.CreateInstance(innerCommandType, request.Payload);

                var requestReturn = await _mediator.Send(innerRequest);

                var resultPayload = new CommandPayload<T>()
                {
                    CommandType = request.CommandType,
                    Payload = requestReturn
                };

                resultPayloads.Add(resultPayload);
            }

            // should go to transaction scope, but SQLLite
            await _dbContext.SaveChangesAsync();

            return resultPayloads;
        }
    }
}
