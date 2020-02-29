using NetCoreCQRSdemo.Domain.DomainBase;
using System;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class Event : BaseEntity
    {
        public Event()
        {
            Id = Guid.NewGuid().ToString();
        }


    }
}
