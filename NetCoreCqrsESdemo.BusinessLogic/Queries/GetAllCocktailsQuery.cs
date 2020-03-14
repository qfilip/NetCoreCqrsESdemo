using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Queries
{
    public class GetAllCocktailsQuery : BaseQuery, IRequest<List<CocktailDto>>
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
                .Include(x => x.Ingredients)
                .ToListAsync();

            return _appMapper.ToDtos(result).ToList();
        }
    }
}
