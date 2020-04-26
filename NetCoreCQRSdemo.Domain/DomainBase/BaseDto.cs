using NetCoreCQRSdemo.Domain.Enumerations;
using System;

namespace NetCoreCQRSdemo.Domain.Dtos
{
    public class BaseDto
    {
        public string Id { get; set; }
        public eEntityType EntityType { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
