using NetCoreCQRSdemo.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public class BaseQueryGeneric
    {
        public readonly ApplicationDbContext context;
        public BaseQueryGeneric(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
