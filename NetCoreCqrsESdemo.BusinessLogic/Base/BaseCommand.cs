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
    public abstract class BaseCommand<TDto> : BaseCommandGeneric, IRequest<TDto>
    {
        public readonly TDto _dto;
        public readonly eCommandType _commandType;
        
        public BaseCommand(TDto dto, eCommandType commandType)
        {
            _dto = dto;
            _commandType = commandType;
        }

        public string SerializeArguments(TDto commandDtoResult)
        {
            return JsonConvert.SerializeObject(commandDtoResult);
        }

        public TDto DeserializeArguments(string args)
        {
            return JsonConvert.DeserializeObject<TDto>(args);
        }

        private string GetHash(string input)
        {
            using var sha1 = new SHA1Managed();
            var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(bytes);
        }

        public async Task LogEvent<TCommand>(ApplicationDbContext dbContext, TCommand command, TDto commandDtoResult) where TCommand : BaseCommand<TDto>
        {
            var @event = new AppEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Arguments = command.SerializeArguments(commandDtoResult),
                CommandHash = GetHash(typeof(TCommand).Name),
                CreatedOn = DateTime.Now,
                CommandType = command._commandType,
                EntityStatus = eEntityStatus.Active
            };
            
            await dbContext.Events.AddAsync(@event);
        }
    }
}
