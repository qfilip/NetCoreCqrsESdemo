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
                var cocktails = await _dbContext.Cocktails
                    .Where(x => x.EntityStatus == eEntityStatus.Active)
                    .Include(x => x.Excerpts)
                    .ToListAsync();

                var response = _appMapper.MultiMap(cocktails, _appMapper.ToDto);
                
                
                return response;
            }
        }
    }
}
