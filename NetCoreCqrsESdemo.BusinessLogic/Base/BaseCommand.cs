using MediatR;
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
    public abstract class BaseCommand<TRequest> : BaseCommandGeneric, IRequest<TRequest>
    {
        public BaseCommand(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public abstract string SerializeArguments();
        public abstract TRequest DeserializeArguments(string args);
        public async Task LogEvent<TCommand>(TCommand command) where TCommand : BaseCommand<TRequest>
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
