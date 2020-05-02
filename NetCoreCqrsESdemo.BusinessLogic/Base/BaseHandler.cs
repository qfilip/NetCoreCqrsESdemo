using MediatR;
using NetCoreCQRSdemo.Domain.Mapping;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Services;
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
        protected readonly ManualMapper _appMapper;
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IMediator _mediator;
        protected readonly CommandService _commandService;

        public BaseHandler(ApplicationDbContext dbContext)
        {
            _appMapper = new ManualMapper();
            _dbContext = dbContext;
        }

        public BaseHandler(IMediator mediator, CommandService commandService, ApplicationDbContext dbContext)
        {
            _appMapper = new ManualMapper();
            _mediator = mediator;
            _commandService = commandService;
            _dbContext = dbContext;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
