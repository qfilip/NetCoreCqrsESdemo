using MediatR;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NetCoreCqrsESdemo.BusinessLogic.Services
{
    public class CommandExecutionService
    {
        private class CommandContainer<T> where T : BaseDto
        {
            public BaseCommand<T> Instance { get; set; }
            public eCommand Definition { get; set; }
        }

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
            var filteredCommands = CleanRequestStack(requests);

            foreach (var command in filteredCommands)
            {
                var requestResult = await _mediator.Send(command);
                var resultPayload = new CommandInfo<T>()
                {
                    // Command = command
                    CommandType = command._commandType,
                    Dto = requestResult
                };

                resultPayloads.Add(resultPayload);
            }
            // should go to transaction scope, but SQLLite
            await _dbContext.SaveChangesAsync();

            return resultPayloads;
        }

        private IEnumerable<BaseCommand<T>> CleanRequestStack<T>(IEnumerable<CommandInfo<T>> requests) where T : BaseDto
        {
            var commands = new List<BaseCommand<T>>();
            var filteredCommands = new List<BaseCommand<T>>();
            
            var commandsT = new List<CommandContainer<T>>();
            var filteredCommandsT = new List<CommandContainer<T>>();

            foreach (var request in requests)
            {
                var innerCommandType = _commandService.GetCommandByEnum(request.Command);
                var innerRequest = (BaseCommand<T>)Activator.CreateInstance(innerCommandType, request.Dto);

                commands.Add(innerRequest);
                commandsT.Add(new CommandContainer<T> { Instance = innerRequest, Definition = request.Command });
            }

            var distinctDtoIds = commands.Select(x => x.Dto.Id).Distinct().ToList();
            foreach(var distinctDtoId in distinctDtoIds)
            {
                var commandsForDto = commands.Where(x => x.Dto.Id == distinctDtoId);
                var commandForDtoTypes = commandsForDto.Select(x => x._commandType).Distinct();

                var actions = eCommandType.None;

                foreach(var commandType in commandForDtoTypes)
                    actions = actions | commandType;

                // ignoreAllCommands //         ignore entity that was created, then deleted locally
                // ignoreChangesOnDeleted //    ignore changes on entity that will be deleted, just delete it
                // ignoreModifications //       if entity was created and then modified locally, just add last version of it

                var ignoreAllCommands = (actions & eCommandType.Create) > 0 && (actions & eCommandType.Delete) > 0;
                var ignoreChangesOnDeleted = (actions & eCommandType.Delete) > 0;
                var ignoreModifications = (actions & eCommandType.Create) > 0 && (actions & eCommandType.Edit) > 0;

                var commandsToExecute = new List<BaseCommand<T>>();
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
                    var lastEditCommandDto = commandsForDto
                        .Where(x => x._commandType == eCommandType.Edit)
                        .Last().Dto;

                    var createCommand = commandsForDto.Where(x => x._commandType == eCommandType.Create).First();
                    createCommand.Dto = lastEditCommandDto;

                    commandsToExecute.Add(createCommand);
                }
                else
                {
                    commandsToExecute = commandsForDto.ToList();
                }

                filteredCommands.AddRange(commandsToExecute);
            
            } // end foreach


            return filteredCommands;
        }

        private (BaseCommand<T>, eCommand) ToTuple<T>(BaseCommand<T> instance, eCommand commandEnum) where T : BaseDto =>
            (instance, commandEnum);
    }
}
