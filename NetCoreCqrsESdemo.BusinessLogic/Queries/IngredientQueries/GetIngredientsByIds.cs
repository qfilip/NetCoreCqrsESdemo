using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Queries.IngredientQueries
{
    public class GetIngredientsByIds : BaseQuery<IEnumerable<IngredientDto>>
    {
        private readonly IEnumerable<Guid> _ingredientIds;
        public GetIngredientsByIds(IEnumerable<Guid> ingredientIds)
        {
            _ingredientIds = ingredientIds;
        }

        public class Validator : AbstractValidator<GetIngredientsByIds>
        {
            public Validator()
            {
                RuleFor(x => x._ingredientIds).NotNull().WithMessage("Ingredient Ids are null");
                RuleFor(x => x._ingredientIds).NotEmpty().WithMessage("Ingredient Ids are empty");
            }
        }

        public class Handler : BaseHandler<GetIngredientsByIds, IEnumerable<IngredientDto>>
        {
            public Handler(ApplicationDbContext dbContext) : base(dbContext) { }

            public override async Task<IEnumerable<IngredientDto>> Handle(GetIngredientsByIds request, CancellationToken cancellationToken)
            {
                var ingredientIds = 
                    _appMapper.MultiMap(request._ingredientIds, _appMapper.GuidToString);

                var ingredients = await _dbContext.Ingredients
                    .Where(x =>
                        x.EntityStatus == eEntityStatus.Active &&
                        ingredientIds.Contains(x.Id))
                    .ToListAsync();

                
                return _appMapper.MultiMap(ingredients, _appMapper.ToDto);
            }
        }
    }
}
