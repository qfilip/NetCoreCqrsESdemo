﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Domain.Mapping;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseCommand
    {
        public ApplicationDbContext dbContext;
        public BaseCommand(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract string SerializeArguments();
        public abstract TDto DeserializeArguments<TDto>(string args) where TDto : BaseDto;
        public async Task LogEvent<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            var @event = new AppEvent()
            {
                Arguments = command.SerializeArguments(),
                CommandCode = (typeof(TCommand)).GetHashCode(),
                CreatedOn = DateTime.Now
            };
            
            await dbContext.Events.AddAsync(@event);
            await dbContext.SaveChangesAsync();
        }
    }
}
