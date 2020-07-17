﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Domain.Mapping;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseCommand<TRequest> : BaseCommandGeneric, IRequest<TRequest>
    {
        public TRequest Dto { get; set; }
        public readonly eCommandType _commandType;
        
        public BaseCommand(TRequest dto, eCommandType commandType)
        {
            Dto = dto;
            _commandType = commandType;
        }

        public string SerializeArguments()
        {
            return JsonConvert.SerializeObject(Dto);
        }
        public TRequest DeserializeArguments(string args)
        {
            return JsonConvert.DeserializeObject<TRequest>(args);
        }

        public async Task LogEvent<TCommand>(TCommand command, ApplicationDbContext dbContext) where TCommand : BaseCommand<TRequest>
        {
            var @event = new AppEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Arguments = command.SerializeArguments(),
                CommandCode = (typeof(TCommand)).GetHashCode(),
                CreatedOn = DateTime.Now,
                CommandType = command._commandType,
                EntityStatus = eEntityStatus.Active
            };
            
            await dbContext.Events.AddAsync(@event);
        }
    }
}
