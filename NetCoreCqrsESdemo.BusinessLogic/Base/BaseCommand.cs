using MediatR;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseCommand<TRequest> : BaseCommandGeneric, IRequest<TRequest>
    {
        public readonly TRequest _dto;
        public readonly eCommandType _commandType;
        
        public BaseCommand(TRequest dto, eCommandType commandType)
        {
            _dto = dto;
            _commandType = commandType;
        }

        public string SerializeArguments()
        {
            return JsonConvert.SerializeObject(_dto);
        }

        public TRequest DeserializeArguments(string args)
        {
            return JsonConvert.DeserializeObject<TRequest>(args);
        }

        private string GetHash(string input)
        {
            using var sha1 = new SHA1Managed();
            var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(bytes);
        }

        public async Task LogEvent<TCommand>(TCommand command, ApplicationDbContext dbContext) where TCommand : BaseCommand<TRequest>
        {
            var @event = new AppEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Arguments = command.SerializeArguments(),
                CommandHash = GetHash(typeof(TCommand).Name),
                CreatedOn = DateTime.Now,
                CommandType = command._commandType,
                EntityStatus = eEntityStatus.Active
            };
            
            await dbContext.Events.AddAsync(@event);
        }
    }
}
