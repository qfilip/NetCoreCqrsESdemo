using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands.IngredientCommands
{
    public class CreateIngredientCommand : BaseCommand<IngredientDto>
    {
        public CreateIngredientCommand(IngredientDto dto) : base(dto, eCommandType.Create) {}
        
        public class Handler : BaseHandler<CreateIngredientCommand, IngredientDto>
        {
            public Handler(ApplicationDbContext dbContext) : base(dbContext) {}

            public override async Task<IngredientDto> Handle(CreateIngredientCommand command, CancellationToken cancellationToken)
            {
                command._dto.Id = Guid.NewGuid().ToString();
                var entity = _appMapper.ToEntity(command._dto);

                await _dbContext.Ingredients.AddAsync(entity);

                return _appMapper.ToDto(entity);
            }
        }
    }

}
