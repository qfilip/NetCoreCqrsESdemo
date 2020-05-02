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
        public GetAllIngredientsQuery()
        {
        }
    }

    public class GetAllIngredientsQueryHandler : BaseHandler<GetAllIngredientsQuery, IEnumerable<IngredientDto>>
    {
        public GetAllIngredientsQueryHandler(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<IngredientDto>> Handle(GetAllIngredientsQuery query, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.Ingredients.ToListAsync();
            return _appMapper.MultiMap(entities, _appMapper.ToDto);
        }
    }
}
