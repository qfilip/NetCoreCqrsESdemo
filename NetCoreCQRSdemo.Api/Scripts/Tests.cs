using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Scripts
{
    public class Tests
    {
        private readonly ApplicationDbContext _context;
        public Tests(ApplicationDbContext context)
        {
            _context = context;
        }

        public int SeedDb()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            
            // generate ingredients
            var allIngredients = CreateInitialIngredients();
            allIngredients.ForEach(x => x.Id = Guid.NewGuid().ToString());

            // generate cocktails
            var darkNstormy = new Cocktail() {
                Id = Guid.NewGuid().ToString(),
                Name = "Dark 'N' Stormy",
                CreatedOn = DateTime.Now,
                EntityStatus = Domain.Enumerations.eEntityStatus.Active
            };
            var moscowMule = new Cocktail() {
                Id = Guid.NewGuid().ToString(),
                Name = "Dark 'N' Moscow Mule",
                CreatedOn = DateTime.Now,
                EntityStatus = Domain.Enumerations.eEntityStatus.Active
            };

            // generate recipes
            var recipes = new List<RecipeExcerpt>();

            var dnsRecipe = new Dictionary<string, int>();
            dnsRecipe.Add("Rum", 5);
            dnsRecipe.Add("Ginger Beer", 5);
            dnsRecipe.Add("Mint", 3);

            var mMuleRecipe = new Dictionary<string, int>();
            mMuleRecipe.Add("Vodka", 5);
            mMuleRecipe.Add("Ginger Beer", 5);
            mMuleRecipe.Add("Mint", 3);

            recipes.AddRange(CreateRecipe(darkNstormy, allIngredients, dnsRecipe));
            recipes.AddRange(CreateRecipe(moscowMule, allIngredients, mMuleRecipe));

            recipes.ForEach(x => x.Id = Guid.NewGuid().ToString());

            // add to DB
            _context.Cocktails.Add(moscowMule);
            _context.Cocktails.Add(darkNstormy);

            _context.Ingredients.AddRange(allIngredients);

            _context.Excerpts.AddRange(recipes);

            var result = _context.SaveChanges();

            return result;
        }

        private List<Ingredient> CreateInitialIngredients()
        {
            var vodka = new Ingredient()
            {
                Name = "Vodka",
                Strength = 40,
                CreatedOn = DateTime.Now
            };
            var rum = new Ingredient()
            {
                Name = "Rum",
                Strength = 40,
                CreatedOn = DateTime.Now
            };
            var gingerBeer = new Ingredient()
            {
                Name = "Ginger Beer",
                Strength = 2,
                CreatedOn = DateTime.Now
            };
            var mint = new Ingredient()
            {
                Name = "Mint",
                Strength = 0,
                CreatedOn = DateTime.Now
            };

            var initialIngredients = new List<Ingredient>();
            initialIngredients.Add(vodka);
            initialIngredients.Add(rum);
            initialIngredients.Add(gingerBeer);
            initialIngredients.Add(mint);

            return initialIngredients;
        }
        private List<RecipeExcerpt> CreateRecipe(Cocktail cocktail, List<Ingredient> allIngredients, Dictionary<string, int> ingredients)
        {
            var recipes = new List<RecipeExcerpt>();
            var items = allIngredients.Where(x => ingredients.Keys.Contains(x.Name)).ToList();
            foreach(var item in items)
            {
                var recipeItem = new RecipeExcerpt()
                {
                    CocktailId = cocktail.Id,
                    IngredientId = item.Id,
                    Amount = ingredients[item.Name]
                };
                recipes.Add(recipeItem);
            }

            return recipes;
        }


        public void GetEvent()
        {
            var @event = _context.Events.FirstOrDefault();
        }
    }
}
