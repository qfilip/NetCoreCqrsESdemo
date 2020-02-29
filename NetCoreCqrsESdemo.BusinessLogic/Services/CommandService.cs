using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Services
{
    public class CommandService
    {
        private IDictionary<int, Type> _applicationCommands;

        public CommandService()
        {
            var commands = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(BaseCommand)));

            _applicationCommands = new Dictionary<int, Type>();

            foreach (var command in commands)
            {
                var commandCode = command.GetHashCode();
                _applicationCommands.Add(commandCode, command);
            }
        }

        public Type GetCommandByCode(int commandCode)
        {
            return _applicationCommands[commandCode];
        }
    }
}
