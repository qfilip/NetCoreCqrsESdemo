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
        private IDictionary<eCommand, Type> _appCommands;
        private IDictionary<int, Type> _appCommandsHashed;
        private IDictionary<eCommand, int> _appCommandsEnumerated;

        public CommandService()
        {
            var baseCommandName = "BaseCommand";
            
            var commands = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(BaseCommandGeneric)) && !type.Name.Contains(baseCommandName));

            var enumCount = Enum.GetNames(typeof(eCommand)).Length;
            var cmdCount = commands.Count();

            _appCommands = new Dictionary<eCommand, Type>();
            _appCommandsHashed = new Dictionary<int, Type>();
            _appCommandsEnumerated = new Dictionary<eCommand, int>();
            
            foreach (var command in commands)
            {
                var commandHash = command.GetHashCode();
                eCommand commandEnum =
                    (eCommand)Enum.Parse(typeof(eCommand), command.Name);

                _appCommands.Add(commandEnum, command);
                _appCommandsHashed.Add(commandHash, command);
                _appCommandsEnumerated.Add(commandEnum, commandHash);
            }
        }

        public Type GetCommandByCode(int commandHash)
        {
            return _appCommandsHashed[commandHash];
        }

        public Type GetCommandByEnum(eCommand commandEnumeration)
        {
            return _appCommands[commandEnumeration];
        }
    }
}
