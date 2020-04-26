using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Queries
{
    public class GetAllCocktailsQuery : BaseQuery<List<CocktailDto>>
    {
        public GetAllCocktailsQuery(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class GetAllCocktailsHandler : BaseHandler<GetAllCocktailsQuery, List<CocktailDto>>
    { 
        public async override Task<List<CocktailDto>> Handle(GetAllCocktailsQuery request, CancellationToken cancellationToken)
        {
            var result = await request.context.Cocktails
                .Include(x => x.Excerpts)
                    .ThenInclude(x => x.Ingredient)
                .ToListAsync();

            return _appMapper.MultiMap(result, _appMapper.ToDto).ToList();
        }
    }
}
