using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands
{
    public class DeleteCocktailCommand : BaseCommand, IRequest<CocktailDto>
    {
        public CocktailDto Dto;
        public DeleteCocktailCommand(ApplicationDbContext dbContext, CocktailDto dto) : base(dbContext)
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

    public class DeleteCocktailHandler : BaseHandler<DeleteCocktailCommand, CocktailDto>
    {
        public override async Task<CocktailDto> Handle(DeleteCocktailCommand request, CancellationToken cancellationToken)
        {
            var entity = await request.dbContext.Cocktails
                .Where(x => x.Id == request.Dto.Id)
                .SingleOrDefaultAsync();

            request.dbContext.Cocktails.Remove(entity);
            await request.LogEvent(request);

            return _appMapper.ToDto(entity);
        }
    }
}
