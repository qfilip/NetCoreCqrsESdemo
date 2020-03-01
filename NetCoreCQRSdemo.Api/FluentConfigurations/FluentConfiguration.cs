using NetCoreCQRSdemo.Domain.DomainBase;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using Reinforced.Typings.Fluent;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NetCoreCQRSdemo.Api.FluentConfigurations
{
    public static class FluentConfiguration
    {
        public static void Configure(ConfigurationBuilder builder)
        {
            var commands = typeof(BaseDto).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseDto)));

            builder.Global(cfg => cfg.CamelCaseForProperties().UseModules());
            builder.ExportAsInterfaces(commands, cfg => cfg.WithPublicProperties());
           
        }
    }
}
