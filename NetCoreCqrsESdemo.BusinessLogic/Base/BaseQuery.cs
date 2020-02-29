using MediatR;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseQuery
    {
        public readonly ApplicationDbContext context;

        public BaseQuery(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
