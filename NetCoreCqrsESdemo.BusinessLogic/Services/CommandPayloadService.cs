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
    public class CommandExecutionService
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;
        private readonly CommandService _commandService;
        
        public CommandExecutionService(IMediator mediator, ApplicationDbContext dbContext, CommandService commandService)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _commandService = commandService;
        }

        public async Task<IEnumerable<CommandInfo<T>>> ParseAndExecute<T>(IEnumerable<CommandInfo<T>> requests) where T : BaseDto
        {
            var resultPayloads = new List<CommandInfo<T>>();

            foreach (var request in requests)
            {
                var innerCommandType = _commandService.GetCommandByEnum(request.Type);
                var innerRequest = (BaseCommand<T>)Activator.CreateInstance(innerCommandType, request.Dto);

                var requestResult = await _mediator.Send(innerRequest);

                var resultPayload = new CommandInfo<T>()
                {
                    Type = request.Type,
                    EventType = innerRequest._eventType,
                    Dto = requestResult
                };

                resultPayloads.Add(resultPayload);
            }

            // should go to transaction scope, but SQLLite
            await _dbContext.SaveChangesAsync();

            return resultPayloads;
        }
    }
}
