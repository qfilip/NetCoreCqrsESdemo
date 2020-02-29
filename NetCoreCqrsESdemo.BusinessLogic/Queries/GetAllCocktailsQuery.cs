using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Queries
{
    public class GetAllCocktailsQuery : BaseQuery, IRequest<List<Cocktail>>
    {
        public GetAllCocktailsQuery(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class GetAllCocktailsHandler : IRequestHandler<GetAllCocktailsQuery, List<Cocktail>>
    {
        public async Task<List<Cocktail>> Handle(GetAllCocktailsQuery request, CancellationToken cancellationToken)
        {
            var result = await request.context.Cocktails
                .Include(x => x.Ingredients)
                .ToListAsync();
            
            return result;
        }
    }
}
