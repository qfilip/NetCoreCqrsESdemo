using MediatR;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
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

        public async Task ParseAndSendPayload<T>(IEnumerable<CommandPayload<T>> payloads) where T : BaseDto
        {
            var commandsToExecute = new List<BaseCommand<T>>();
            foreach (var request in payloads)
            {
                var innerCommandType = _commandService.GetCommandByEnum(request.CommandType);
                var innerCommand = (BaseCommand<T>)Activator.CreateInstance(innerCommandType, request.Payload);
                commandsToExecute.Add(innerCommand);
            }

            // should go to transaction scope, but SQLLite
            foreach (var cmd in commandsToExecute)
            {
                await _mediator.Send(cmd);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
