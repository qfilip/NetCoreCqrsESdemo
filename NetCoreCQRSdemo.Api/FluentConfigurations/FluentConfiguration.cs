using NetCoreCQRSdemo.Domain.DomainBase;
using Reinforced.Typings.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.FluentConfigurations
{
    public static class FluentConfiguration
    {
        public static void Configure(ConfigurationBuilder builder)
        {
            // first get all that inherits from base dto
            builder.ExportAsInterface<BaseDto>()
                .WithPublicProperties(cfg => cfg.CamelCase())
                .WithPublicFields(cfg => cfg.CamelCase());
        }
    }
}
