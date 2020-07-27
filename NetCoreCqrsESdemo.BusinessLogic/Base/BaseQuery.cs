using MediatR;
using NetCoreCQRSdemo.Domain.Mapping;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseQuery<TResponse> : IRequest<TResponse>
    {
    }
}
