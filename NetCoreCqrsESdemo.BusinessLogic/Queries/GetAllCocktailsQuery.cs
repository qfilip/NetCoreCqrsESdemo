using MediatR;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Queries
{
    public class GetAllCocktailsQuery : IRequest<List<Cocktail>>
    {
        public ApplicationDbContext Context;
        public GetAllCocktailsQuery(ApplicationDbContext context)
        {
            Context = context;
        }
    }

    public class GetAllCocktailsHandler : IRequestHandler<GetAllCocktailsQuery, List<Cocktail>>
    {
        public async Task<List<Cocktail>> Handle(GetAllCocktailsQuery request, CancellationToken cancellationToken)
        {
            var cocktail = new Cocktail {
                Id = "12"
            };
            await Task.Delay(50);
            var list = new List<Cocktail>();
            list.Add(cocktail);
            return list;
        }
    }
}
