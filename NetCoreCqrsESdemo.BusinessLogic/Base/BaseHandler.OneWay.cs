using MediatR;
using NetCoreCQRSdemo.Domain.Mapping;
using NetCoreCQRSdemo.Persistence.Context;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseHandler<TRequest> : AsyncRequestHandler<TRequest>
       where TRequest : IRequest
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

        public BaseHandler(ApplicationDbContext dbContext) : this()
        {
            _dbContext = dbContext;
        }

        public BaseHandler(ApplicationDbContext dbContext, IAppMapper mapper = null, IMediator mediator = null)
           : this(dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
