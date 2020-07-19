using FluentValidation;
using FluentValidation.Internal;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands.IngredientCommands
{
    public class EditIngredientCommand : BaseCommand<IngredientDto>
    {
        public EditIngredientCommand(IngredientDto dto) : base(dto, eCommandType.Edit) { }
    }

    public class EditIngredientValidator : AbstractValidator<EditIngredientCommand>
    {
        public EditIngredientValidator()
        {
            RuleFor(x => x._dto.Id).NotNull();
            RuleFor(x => x._dto.Id).NotEmpty();
        }
    }

    public class EditIngredientCommandHandler : BaseHandler<EditIngredientCommand, IngredientDto>
    {
        public EditIngredientCommandHandler(ApplicationDbContext dbContext) : base(dbContext) {}

        public override async Task<IngredientDto> Handle(EditIngredientCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Ingredients
                .Where(x => x.Id == command._dto.Id).SingleOrDefaultAsync();

            entity.Name = command._dto.Name;
            entity.Strength = command._dto.Strength;

            return _appMapper.ToDto(entity);
        }
    }
}
