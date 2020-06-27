using NetCoreCQRSdemo.Domain.Dtos;
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
        public CreateIngredientCommand(IngredientDto dto) : base(dto) {}
    }

    public class CreateIngredientCommandHandler : BaseHandler<CreateIngredientCommand, IngredientDto>
    {
        public CreateIngredientCommandHandler(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IngredientDto> Handle(CreateIngredientCommand command, CancellationToken cancellationToken)
        {
            command.Request.Id = Guid.NewGuid().ToString();
            var entity = _appMapper.ToEntity(command.Request);

            await _dbContext.Ingredients.AddAsync(entity);
            await command.LogEvent(command, _dbContext);

            return _appMapper.ToDto(entity);
        }
    }
}
