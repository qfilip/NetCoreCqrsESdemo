using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.Enumerations
{
    public enum eCommandType
    {
        None = 0,
        Create = 1,
        Edit = 2,
        Delete = 4
    }
}
