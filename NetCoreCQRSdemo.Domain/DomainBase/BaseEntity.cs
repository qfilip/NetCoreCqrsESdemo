using System;

namespace NetCoreCQRSdemo.Domain.DomainBase
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string CreatedOn { get; set; }
    }
}
