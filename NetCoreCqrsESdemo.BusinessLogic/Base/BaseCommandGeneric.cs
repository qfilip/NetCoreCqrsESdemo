using NetCoreCQRSdemo.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public class BaseCommandGeneric
    {
        public ApplicationDbContext dbContext;
        public BaseCommandGeneric(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
