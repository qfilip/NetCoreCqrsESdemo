using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands.CocktailCommands
{
    public class CreateCocktailCommand : BaseCommand<CocktailDto>
    {
        public CreateCocktailCommand(CocktailDto dto) : base (dto, eCommandType.Create) { }

        public class Handler : BaseHandler<CreateCocktailCommand, CocktailDto>
        {
            public Handler(ApplicationDbContext dbContext) : base(dbContext) { }

            public async override Task<CocktailDto> Handle(CreateCocktailCommand request, CancellationToken cancellationToken)
            {
                var entityId = Guid.NewGuid().ToString();

                var entity = _appMapper.ToEntity(request._dto);
                entity.Id = entityId;

                foreach (var excerpt in entity.Excerpts)
                {
                    excerpt.Id = Guid.NewGuid().ToString();
                    excerpt.CocktailId = entityId;
                }

                await _dbContext.AddAsync(entity);
                var result = _appMapper.ToDto(entity);


                return result;
            }
        }
    }
}
