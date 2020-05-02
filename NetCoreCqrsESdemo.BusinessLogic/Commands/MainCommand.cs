using MediatR;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands
{
    public class MainCommand<T> : BaseCommand<IEnumerable<CommandPayload<T>>> where T : BaseDto
    {
        public MainCommand(IEnumerable<CommandPayload<T>> requests) : base(requests)
        {
        }
    }

    public class MainCommandHandler<T> : BaseHandler<MainCommand<T>, IEnumerable<CommandPayload<T>>> where T : BaseDto
    {
        public MainCommandHandler(IMediator mediator, CommandService commandService, ApplicationDbContext dbContext)
            : base(mediator, commandService, dbContext)
        {
        }

        public override async Task<IEnumerable<CommandPayload<T>>> Handle(MainCommand<T> command, CancellationToken cancellationToken)
        {
            var commandsToExecute = new List<BaseCommand<T>>();
            foreach(var request in command.Request)
            {
                var innerCommandType = _commandService.GetCommandByEnum(request.CommandType);
                var innerCommand = (BaseCommand<T>) Activator.CreateInstance(innerCommandType, request.Payload);
                commandsToExecute.Add(innerCommand);
            }

            using(var scope = new TransactionScope())
            {
                foreach(var cmd in commandsToExecute)
                {
                    await _mediator.Send(cmd);
                }
                await _dbContext.SaveChangesAsync();
                scope.Complete();
            }

            return command.Request;
        }
    }
}
