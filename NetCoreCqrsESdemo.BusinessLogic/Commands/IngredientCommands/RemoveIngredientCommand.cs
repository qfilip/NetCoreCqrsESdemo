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


        public class Validator : AbstractValidator<RemoveIngredientCommand>
        {
            public Validator()
            {
                RuleFor(x => x._dto.Id).NotNull();
                RuleFor(x => x._dto.Id).NotEmpty();
            }

        }


        public class Handler : BaseHandler<RemoveIngredientCommand, IngredientDto>
        {
            public Handler(ApplicationDbContext dbContext) : base(dbContext) { }

            public async override Task<IngredientDto> Handle(RemoveIngredientCommand request, CancellationToken cancellationToken)
            {
                var entity = await _dbContext.Ingredients
                    .FirstOrDefaultAsync(x => x.Id == request._dto.Id);

                entity.EntityStatus = eEntityStatus.Removed;

                return _appMapper.ToDto(entity);
            }
        }
    }
}
