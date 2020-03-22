using MediatR;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands.SnapshotCommands
{
    public class SetInitialStateCommand : BaseCommand, IRequest<SimpleResponse>
    {
        public List<CocktailDto> Dtos;
        public SetInitialStateCommand(ApplicationDbContext dbContext) : base(dbContext) { }

        public override SimpleResponse DeserializeArguments<SimpleResponse>(string args)
        {
            return new SimpleResponse() { Id = Guid.NewGuid().ToString() };
        }

        public override string SerializeArguments()
        {
            return JsonConvert.SerializeObject(Dtos);
        }
    }

    public class SetInitialStateHandler : BaseHandler<SetInitialStateCommand, SimpleResponse>
    {
        public override async Task<SimpleResponse> Handle(SetInitialStateCommand request, CancellationToken cancellationToken)
        {
            request.dbContext.Database.EnsureDeleted();
            request.dbContext.Database.EnsureCreated();

            var rand = new Random();
            var names = new string[] { "Moscow Mule", "Dark n Stormy", "Negroni" };
            var cocktails = new List<Cocktail>();

            var ingredients = new List<Ingredient>()
                {
                    new Ingredient() {  Name = "Vodka", Amount = 5, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Ginger Beer", Amount = 3, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Triple Sec", Amount = 2, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Rum", Amount = 5, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Ginger Beer", Amount = 3, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Triple Sec", Amount = 2, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Gin", Amount = 2, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Vermouth", Amount = 2, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Triple Sec", Amount = 2, UnitOfMeasure = "cl" }
                };

            for (int i = 0; i < names.Length; i++)
            {
                var cocktail = new Cocktail()
                {
                    Name = names[i],
                    Strength = rand.Next(0, 101),
                    CreatedOn = DateTime.Now
                };

                var cocktailIngredients = ingredients.Skip(i * 3).Take(3).ToList();
                cocktailIngredients.ForEach(x => x.CocktailId = cocktail.Id);
                cocktail.Ingredients = cocktailIngredients;

                cocktails.Add(cocktail);
            }

            request.dbContext.Cocktails.AddRange(cocktails);
            await request.dbContext.SaveChangesAsync();

            return new SimpleResponse() { Message = "Done", Success = true };
        }
    }
}
