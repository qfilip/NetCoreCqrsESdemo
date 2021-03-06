﻿using NetCoreCQRSdemo.Api.ProjectConfigurations;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCqrsESdemo.BusinessLogic.Base;
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
            var dtos = Assembly.GetAssembly(typeof(Domain.Dtos.BaseDto)).ExportedTypes
                .Where(i => i.Namespace.StartsWith(GlobalVariables.NMSP_DomainDtos))
                .OrderBy(i => i.Name)
                .OrderBy(i => i.Name != nameof(Domain.Dtos.BaseDto))
                .ToArray();

            var payload = Assembly.GetAssembly(typeof(CommandInfo<>))
                .ExportedTypes
                .Where(i => i == typeof(CommandInfo<>))
                .OrderBy(i => i.Name)
                .ToArray();

            builder.Global(cfg => cfg.CamelCaseForProperties().UseModules());

            builder.ExportAsInterfaces(dtos, cfg =>
                cfg.WithPublicProperties()
                .ExportTo("interfaces.ts"));

            builder.ExportAsInterfaces(payload, cfg =>
                cfg.WithPublicProperties()
                .ExportTo("interfaces.ts"));

            builder.ExportAsEnums(new Type[] {
                typeof(eCommand),
                typeof(eCommandType)
            },
            cfg => cfg.ExportTo("enums.ts"));
        }
    }
}
