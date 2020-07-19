using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
    public class RemoveIngredientCommand : BaseCommand<IngredientDto>
    {
        public RemoveIngredientCommand(IngredientDto dto) : base(dto, eCommandType.Remove) {}
    }

    public class RemoveIngredientValidator : AbstractValidator<RemoveIngredientCommand>
    {
        public RemoveIngredientValidator()
        {
            RuleFor(x => x._dto.Id).NotNull();
            RuleFor(x => x._dto.Id).NotEmpty();
        }
    }

    public class RemoveIngredientCommandHandler : BaseHandler<RemoveIngredientCommand, IngredientDto>
    {
        public RemoveIngredientCommandHandler(ApplicationDbContext dbContext) : base(dbContext) {}

        public async override Task<IngredientDto> Handle(RemoveIngredientCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Ingredients
                .FirstOrDefaultAsync(x => x.Id == request._dto.Id);

            entity.EntityStatus = eEntityStatus.Removed;

            return _appMapper.ToDto(entity);
        }
    }
}
