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

namespace NetCoreCqrsESdemo.BusinessLogic.Queries.CocktailQueries
{
    public class GetAllCocktailsQuery : BaseQuery<IEnumerable<CocktailDto>>
    {
        public GetAllCocktailsQuery()
        {

        }


        public class Handler : BaseHandler<GetAllCocktailsQuery, IEnumerable<CocktailDto>>
        {
            public Handler(ApplicationDbContext dbContext) : base(dbContext) { }

            public async override Task<IEnumerable<CocktailDto>> Handle(GetAllCocktailsQuery request, CancellationToken cancellationToken)
            {
                var entities = await _dbContext.Cocktails
                    .ToListAsync();

                var response = _appMapper.MultiMap(entities, _appMapper.ToDto);

                
                return response;
            }
        }
    }
}
