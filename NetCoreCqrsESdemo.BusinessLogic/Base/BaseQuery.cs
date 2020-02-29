using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseQuery<TResponse> : IRequest<TResponse>
    {
        public BaseQuery()
        {
        }
    }

    public abstract class BaseQuery : IRequest
    {
        public BaseQuery()
        {
        }
    }
}
