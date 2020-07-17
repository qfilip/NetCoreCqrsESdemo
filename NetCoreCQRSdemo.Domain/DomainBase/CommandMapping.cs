using NetCoreCQRSdemo.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.DomainBase
{
    public class CommandMap
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public Type Type { get; set; }
        public eCommand Enumeration { get; set; }
    }
}
