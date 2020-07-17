using NetCoreCQRSdemo.Domain.DomainBase;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Services
{
    public class CommandService
    {
        private readonly List<CommandMap> _map;
        
        public CommandService(string appMappings)
        {
            _map = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<CommandMap>>(appMappings);
        }

        public Type GetCommandByCode(string commandHash)
        {
            return _map.Where(x => x.Hash == commandHash)
                .Select(x => x.Type).FirstOrDefault();
        }

        public Type GetCommandByEnum(eCommand commandEnumeration)
        {
            return _map.Where(x => x.Enumeration == commandEnumeration)
                .Select(x => x.Type).FirstOrDefault();
        }
    }
}
