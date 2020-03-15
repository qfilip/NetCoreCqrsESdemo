using NetCoreCQRSdemo.Domain.Dtos;
using System;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class AppEvent : BaseEntity
    {
        public string Arguments { get; set; }
        public int CommandCode { get; set; }
    }
}
