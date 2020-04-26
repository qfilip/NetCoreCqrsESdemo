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
    public class GetAllIngredientsQuery : BaseQuery<IEnumerable<IngredientDto>>
    {
        public GetAllIngredientsQuery(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class GetAllIngredientsQueryHandler : BaseHandler<GetAllIngredientsQuery, IEnumerable<IngredientDto>>
    {
        public override async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var ingredients = await request.context.Ingredients
                .Where(x => x.EntityStatus == eEntityStatus.Active)
                .ToListAsync();

            return _appMapper.MultiMap(ingredients, _appMapper.ToDto);
        }
    }
}
