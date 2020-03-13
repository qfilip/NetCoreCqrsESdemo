using NetCoreCQRSdemo.Api.ProjectConfigurations;
using NetCoreCQRSdemo.Domain.Enumerations;
using Reinforced.Typings.Fluent;
using System;
using System.Linq;
using System.Reflection;

namespace NetCoreCQRSdemo.Api.FluentConfigurations
{
    public static class FluentConfiguration
    {
        public static void Configure(ConfigurationBuilder builder)
        {
            //var commands = typeof(BaseDto).Assembly.GetTypes()
            //    .Where(t => t.IsSubclassOf(typeof(BaseDto)));

            var dtos = Assembly.GetAssembly(typeof(NetCoreCQRSdemo.Domain.Dtos.BaseDto)).ExportedTypes
                .Where(i => i.Namespace.StartsWith(GlobalVariables.NMSP_DomainDtos))
                .OrderBy(i => i.Name)
                .OrderBy(i => i.Name != nameof(NetCoreCQRSdemo.Domain.Dtos.BaseDto))
                .ToArray();

            builder.Global(cfg => cfg.CamelCaseForProperties().UseModules());

            builder.ExportAsInterfaces(dtos, cfg =>
                cfg.WithPublicProperties()
                .ExportTo("interfaces.ts"));

            builder.ExportAsEnums(new Type[] {
                typeof(eCommand)
            },
            cfg => cfg.ExportTo("enums.ts"));
        }
    }
}
