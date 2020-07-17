using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;
using System;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class AppEvent : BaseEntity
    {
        public string Arguments { get; set; }
        public string CommandHash { get; set; }
        public eCommandType CommandType { get; set; }
    }
}
