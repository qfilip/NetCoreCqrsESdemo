﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.Enumerations
{
    public enum eEntityStatus
    {
        Active,
        Historical,
        Removed = 99
    }
}
