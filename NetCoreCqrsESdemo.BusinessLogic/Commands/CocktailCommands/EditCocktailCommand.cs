using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands.CocktailCommands
{
    public class EditCocktailCommand : BaseCommand<CocktailDto>
    {
        public EditCocktailCommand(CocktailDto dto) : base(dto, eCommandType.Edit)
        {

        }

        public class Handler : BaseHandler<EditCocktailCommand, CocktailDto>
        {
            public Handler(ApplicationDbContext dbContext) : base(dbContext) { }

            public async override Task<CocktailDto> Handle(EditCocktailCommand request, CancellationToken cancellationToken)
            {   
                // data fetch & calcs
                var cocktail = await _dbContext.Cocktails
                    .Where(x => x.Id == request._dto.Id)
                    .Include(x => x.Excerpts)
                    .SingleAsync();

                var oldExcerptIds = cocktail.Excerpts
                    .Select(x => x.Id)
                    .ToList();

                var newExcerptIds = request._dto.Excerpts
                    .Select(x => x.Id)
                    .ToList();

                var excerptsToAddIds = newExcerptIds.Except(oldExcerptIds);
                var excerptsToRemoveIds = oldExcerptIds.Except(newExcerptIds);

                // change handling
                cocktail.Name = request._dto.Name;

                var excerptsToAdd = request._dto.Excerpts
                    .Where(x => excerptsToAddIds.Contains(x.Id));

                foreach(var e in excerptsToAdd)
                {
                    var toAdd = new RecipeExcerpt
                    {
                        Id = Guid.NewGuid().ToString(),
                        Amount = e.Amount,
                        CocktailId = e.CocktailId,
                        IngredientId = e.IngredientId
                    };

                    await _dbContext.Excerpts.AddAsync(toAdd);
                }

                var excerptsToRemove = cocktail.Excerpts
                    .Where(x => excerptsToRemoveIds.Contains(x.Id));

                _dbContext.Excerpts.RemoveRange(excerptsToRemove);
                

                return _appMapper.ToDto(cocktail);
            }
        }
    }
}
