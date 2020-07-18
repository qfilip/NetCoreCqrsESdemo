using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                var requestResult = await _mediator.Send(command.Instance);
                var resultPayload = new CommandInfo<T>()
                {
                    Command = command.Definition,
                    CommandType = command.Instance._commandType,
                    Dto = requestResult
                };

                resultPayloads.Add(resultPayload);
            }
            // should go to transaction scope, but SQLLite
            await _dbContext.SaveChangesAsync();

            return resultPayloads;
        }

        private IEnumerable<CommandContainer<T>> CleanRequestStack<T>(IEnumerable<CommandInfo<T>> requests) where T : BaseDto
        {   
            var commands = new List<CommandContainer<T>>();
            var filteredCommands = new List<CommandContainer<T>>();

            foreach (var request in requests)
            {
                var innerCommandType = _commandService.GetCommandByEnum(request.Command);
                var innerRequest = (BaseCommand<T>)Activator.CreateInstance(innerCommandType, request.Dto);

                commands.Add(new CommandContainer<T> { Instance = innerRequest, Definition = request.Command });
            }

            var distinctDtoIds = commands.Select(x => x.Instance._dto.Id).Distinct().ToList();
            foreach(var distinctDtoId in distinctDtoIds)
            {
                var commandsForDto = commands.Where(x => x.Instance._dto.Id == distinctDtoId).ToList();
                var commandForDtoTypes = commandsForDto.Select(x => x.Instance._commandType).Distinct();

                var actions = eCommandType.None;

                foreach(var commandType in commandForDtoTypes)
                    actions = actions | commandType;

                // ignoreAllCommands //         ignore entity that was created, then deleted locally
                // ignoreChangesOnDeleted //    ignore changes on entity that will be deleted, just delete it
                // ignoreModifications //       if entity was created and then modified locally, just add last version of it

                var ignoreAllCommands = (actions & eCommandType.Create) > 0 && (actions & eCommandType.Delete) > 0;
                var ignoreChangesOnDeleted = (actions & eCommandType.Delete) > 0;
                var ignoreModifications = (actions & eCommandType.Create) > 0 && (actions & eCommandType.Edit) > 0;

                var commandsToExecute = new List<CommandContainer<T>>();
                if (ignoreAllCommands)
                {
                    // do nothing
                }
                else if (ignoreChangesOnDeleted)
                {
                    // delete command must be last and single ?
                    var deleteCommand = commandsForDto
                        .Select(x => x.Instance)
                        .Where(x => x._commandType == eCommandType.Delete)
                        .Single();

                    var deleteCommandIndex = FindCommandIndex(commandsForDto, eCommandType.Delete);
                    var commandEnum = commandsForDto[deleteCommandIndex].Definition;

                    commandsToExecute.Add(new CommandContainer<T> { Instance = deleteCommand, Definition = commandEnum });
                }
                else if(ignoreModifications)
                {
                    var lastEditCommandIndex = FindCommandIndex(commandsForDto, eCommandType.Edit);

                    if(lastEditCommandIndex == -1)
                    {
                        throw new ArgumentException("Edit command for Dto not found ");
                    }

                    var lastDtoVersion = commandsForDto.Select(x => x.Instance).ToList()[lastEditCommandIndex]._dto;

                    // create command must be on first position and single?
                    if(commandsForDto[0].Instance._commandType != eCommandType.Create)
                    {
                        throw new InvalidEnumArgumentException("Create command must be first");
                    }

                    var commandEnum = commandsForDto[0].Definition;
                    var commandType = _commandService.GetCommandByEnum(commandEnum);
                    var createCommand = (BaseCommand<T>)Activator.CreateInstance(commandType, lastDtoVersion);

                    commandsToExecute.Add(new CommandContainer<T> { Instance = createCommand, Definition = commandEnum });
                }
                else
                {
                    commandsToExecute = commandsForDto;
                }

                filteredCommands.AddRange(commandsToExecute);
            
            } // end foreach


            return filteredCommands;
        }

        private int FindCommandIndex<T>(List<CommandContainer<T>> container, eCommandType commandType) where T : BaseDto
        {
            var instances = container.Select(x => x.Instance).ToList();
            var instanceCount = instances.Count;
            
            var position = -1;
            for (var i = 0; i < instanceCount; i++)
            {
                if (instances[i]._commandType == commandType)
                {
                    position = i;
                }
            }

            return position;
        }
    }
}
