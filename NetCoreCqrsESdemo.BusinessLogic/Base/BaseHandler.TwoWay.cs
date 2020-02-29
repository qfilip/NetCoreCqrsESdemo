using MediatR;
using NetCoreCQRSdemo.Domain.Mapping;
using NetCoreCQRSdemo.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        #region Fields

        protected readonly ApplicationDbContext _dbContext;
        protected readonly IAppMapper _mapper;
        protected readonly IMediator _mediator;

        #endregion Fields

        // Ctors

        public BaseHandler()
        {
        }

        public BaseHandler(ApplicationDbContext dbContext)
            : this()
        {
            _dbContext = dbContext;
        }

        public BaseHandler(ApplicationDbContext dbContext, IAppMapper mapper = null, IMediator mediator = null)
           : this(dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public BaseHandler(ApplicationDbContext dbContext, IMediator mediator = null)
           : this(dbContext)
        {
            _mediator = mediator;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
