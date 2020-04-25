using MediatR;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands.SnapshotCommands
{
    public class SetInitialStateCommand : BaseCommand, IRequest<SimpleResponse>
    {
        public List<CocktailDto> Dtos;
        public SetInitialStateCommand(ApplicationDbContext dbContext) : base(dbContext) { }

        public override SimpleResponse DeserializeArguments<SimpleResponse>(string args)
        {
            return new SimpleResponse() { Id = Guid.NewGuid().ToString() };
        }

        public override string SerializeArguments()
        {
            return JsonConvert.SerializeObject(Dtos);
        }
    }

    public class SetInitialStateHandler : BaseHandler<SetInitialStateCommand, SimpleResponse>
    {
        public override async Task<SimpleResponse> Handle(SetInitialStateCommand request, CancellationToken cancellationToken)
        {
            request.dbContext.Database.EnsureDeleted();
            request.dbContext.Database.EnsureCreated();

            return new SimpleResponse() { Message = "Done", Success = true };
        }
    }
}
