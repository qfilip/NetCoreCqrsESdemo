using MediatR;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
                var innerCommandType = _commandService.GetCommandByEnum(request.Command);
                var innerRequest = (BaseCommand<T>)Activator.CreateInstance(innerCommandType, request.Dto);

                var requestResult = await _mediator.Send(innerRequest);

                var resultPayload = new CommandInfo<T>()
                {
                    Command = request.Command,
                    CommandType = innerRequest._commandType,
                    Dto = requestResult
                };

                resultPayloads.Add(resultPayload);
            }

            // should go to transaction scope, but SQLLite
            await _dbContext.SaveChangesAsync();

            return resultPayloads;
        }

        private void CleanRequestStack<T>(IEnumerable<CommandInfo<T>> requests) where T : BaseDto
        {
            var commands = new List<BaseCommand<T>>();
            var filteredCommands = new List<BaseCommand<T>>();

            foreach (var request in requests)
            {
                var innerCommandType = _commandService.GetCommandByEnum(request.Command);
                var innerRequest = (BaseCommand<T>)Activator.CreateInstance(innerCommandType, request.Dto);

                commands.Add(innerRequest);
            }

            var distinctDtoIds = commands.Select(x => x._dto.Id).Distinct().ToList();
            foreach(var distinctDtoId in distinctDtoIds)
            {
                var commandsForDto = commands.Where(x => x._dto.Id == distinctDtoId);
                var commandForDtoTypes = commandsForDto.Select(x => x._commandType).Distinct();

                var actions = eCommandType.None;

                foreach(var commandType in commandForDtoTypes)
                    actions = actions | commandType;

                // ignoreAllCommands //         ignore entity that was created, then deleted locally
                // ignoreChangesOnDeleted //    ignore changes on entity that will be deleted, just delete it
                // ignoreModifications //       if entity was created, then modified, just add last version of it

                var ignoreAllCommands = (actions & eCommandType.Create) > 0 && (actions & eCommandType.Delete) > 0;
                var ignoreChangesOnDeleted = (actions & eCommandType.Delete) > 0;
                var ignoreModifications = (actions & eCommandType.Create) > 0 && (actions & eCommandType.Edit) > 0;

                List<BaseCommand<T>> commandsToExecute = null;
                if (ignoreAllCommands)
                {
                    // do nothing
                }
                else if (ignoreChangesOnDeleted)
                {
                    commandsToExecute = commandsForDto.Where(x => x._commandType == eCommandType.Delete).ToList();
                }
                else if(ignoreModifications)
                {
                    var lastEditCommand = commandsForDto
                        .Where(x => x._commandType == eCommandType.Edit)
                        .Last();
                    // create mappings of all commands (Base Generic) with maps of eCommand & eCommandType, then fetch instance
                    // var commandToExecute = (BaseCommand<T>)Activator.CreateInstance(innerCommandType, request.Dto);
                }
                else
                {

                }

                if(commandsToExecute != null)
                {
                    filteredCommands.AddRange(commandsToExecute);
                }
            }
        }
    }
}
