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
        private IDictionary<int, Type> _appCommands;
        private IDictionary<eCommand, int> _appCommandsEnumerated;

        public CommandService()
        {
            var commands = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(BaseCommandGeneric)));

            _appCommands = new Dictionary<int, Type>();
            _appCommandsEnumerated = new Dictionary<eCommand, int>();
            
            foreach (var command in commands)
            {
                var commandCode = command.GetHashCode();
                eCommand commandEnum =
                    (eCommand)Enum.Parse(typeof(eCommand), command.Name);
                
                _appCommands.Add(commandCode, command);
                _appCommandsEnumerated.Add(commandEnum, commandCode);
            }
        }

        public Type GetCommandByCode(int commandCode)
        {
            return _appCommands[commandCode];
        }

        public int GetCommandCodeByEnum(eCommand commandEnumeration)
        {
            return _appCommandsEnumerated[commandEnumeration];
        }
    }
}
