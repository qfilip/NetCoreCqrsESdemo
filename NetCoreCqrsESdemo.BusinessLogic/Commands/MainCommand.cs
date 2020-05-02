using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands
{
    public class MainCommand<T> : BaseCommand<IEnumerable<CommandPayload<T>>> where T : BaseDto
    {
        public MainCommand(IEnumerable<CommandPayload<T>> requests) : base(requests)
        {
        }
    }

    public class MainCommandHandler<T> : BaseHandler<MainCommand<T>, IEnumerable<CommandPayload<T>>> where T : BaseDto
    {
        public MainCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<CommandPayload<T>>> Handle(MainCommand<T> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
