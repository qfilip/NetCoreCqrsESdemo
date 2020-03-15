using MediatR;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands
{
    public class CreateCocktailCommand : BaseCommand, IRequest<CocktailDto>
    {
        public CocktailDto Dto;
        public CreateCocktailCommand(ApplicationDbContext dbContext, CocktailDto dto) : base(dbContext)
        {
            Dto = dto;
        }

        public override CocktailDto DeserializeArguments<CocktailDto>(string args)
        {
            return JsonConvert.DeserializeObject<CocktailDto>(args);
        }

        public override string SerializeArguments()
        {
            return JsonConvert.SerializeObject(Dto);
        }
    }

    public class CreateCocktailHandler : BaseHandler<CreateCocktailCommand, CocktailDto>
    {
        public override async Task<CocktailDto> Handle(CreateCocktailCommand request, CancellationToken cancellationToken)
        {
            var entity = _appMapper.ToEntity(request.Dto);
            entity.Id = Guid.NewGuid().ToString();
            
            await request.dbContext.AddAsync(entity);
            await request.LogEvent(request);
            
            return request.Dto;
        }
    }
}
