using NetCoreCQRSdemo.Domain.DomainBase;
using NetCoreCQRSdemo.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetCoreCqrsESdemo.BusinessLogic.Services
{
    public class CommandService
    {
        private readonly List<CommandMap> _map;

        public CommandService(string appMappings)
        {
            var mappingFileContent = File.ReadAllText(appMappings);
            
            _map = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<CommandMap>>(mappingFileContent);
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
