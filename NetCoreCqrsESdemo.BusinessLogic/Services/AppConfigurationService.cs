using Microsoft.Extensions.Hosting;
using NetCoreCQRSdemo.Domain.DomainBase;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Services
{
    public class AppConfigurationService : IHostedService
    {
        private readonly string _mappingsFilePath;
        public AppConfigurationService(string mappingsFilePath)
        {
            _mappingsFilePath = mappingsFilePath;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            var baseCommandName = "BaseCommand";

            var commands = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(BaseCommandGeneric)) && !type.Name.Contains(baseCommandName));

            var enumCount = Enum.GetNames(typeof(eCommand)).Length;
            var cmdCount = commands.Count();

            //if (enumCount != cmdCount)
            //{
            //    Environment.FailFast("Number of commands not matching number of command enumerations");
            //}

            var mappings = new List<CommandMap>();

            foreach (var command in commands)
            {
                var commandHash = command.GetHashCode();
                eCommand commandEnum = (eCommand)Enum.Parse(typeof(eCommand), command.Name);
                
                var map = new CommandMap
                {
                    Name = command.Name,
                    Hash = GetHash(command.Name),
                    Enumeration = commandEnum,
                    Type = command
                };

                mappings.Add(map);
            }

            var s = Newtonsoft.Json.JsonConvert.SerializeObject(mappings);
            System.IO.File.WriteAllText(_mappingsFilePath, s);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(0);
        }

        private string GetHash(string input)
        {
            using var sha1 = new SHA1Managed();
            var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(bytes);
        }
    }
}
