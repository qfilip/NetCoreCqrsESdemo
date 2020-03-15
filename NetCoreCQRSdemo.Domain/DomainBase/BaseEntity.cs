﻿using System;

namespace NetCoreCQRSdemo.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
