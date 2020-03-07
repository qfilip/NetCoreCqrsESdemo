using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class EventCount : BaseEntity
    {
        public int CurrentCount { get; set; }
    }
}
