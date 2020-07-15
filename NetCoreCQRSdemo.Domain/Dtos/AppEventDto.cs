using NetCoreCQRSdemo.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.Dtos
{
    public class AppEventDto : BaseDto
    {
        public string Arguments { get; set; }
        public int CommandCode { get; set; }
        public eCommandType CommandType { get; set; }
    }
}
