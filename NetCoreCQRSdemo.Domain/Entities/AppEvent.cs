using NetCoreCQRSdemo.Domain.Dtos;
using System;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class AppEvent : BaseEntity
    {
        public AppEvent()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Arguments { get; set; }
        public int CommandCode { get; set; }
        public int OrderNumber { get; set; }
    }
}
