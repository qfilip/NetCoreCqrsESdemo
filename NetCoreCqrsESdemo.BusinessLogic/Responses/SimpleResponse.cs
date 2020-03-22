using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Responses
{
    public class SimpleResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
