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

            var rand = new Random();
            var names = new string[] { "Moscow Mule", "Dark n Stormy", "Negroni" };

            var ings = new List<Ingredient>()
                {
                    new Ingredient() {  Name = "Vodka", Amount = 5, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Ginger Beer", Amount = 3, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Triple Sec", Amount = 2, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Rum", Amount = 5, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Ginger Beer", Amount = 3, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Triple Sec", Amount = 2, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Gin", Amount = 2, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Vermouth", Amount = 2, UnitOfMeasure = "cl" },
                    new Ingredient() {  Name = "Triple Sec", Amount = 2, UnitOfMeasure = "cl" },
                };

            for (int i = 0; i < names.Length; i++)
            {
                var c = new Cocktail()
                {
                    Name = names[i],
                    Strength = rand.Next(0, 101),
                    CreatedOn = DateTime.Now
                };

                var cIngs = ings.Skip(i * 3).Take(3).ToList();
                cIngs.ForEach(x => x.CocktailId = c.Id);
                c.Ingredients = cIngs;

                _context.Cocktails.AddRange(c);
            }

            var @event = new AppEvent()
            {
                Id = Guid.NewGuid().ToString(),
                Arguments = "testargs",
                CommandCode = 0,
                CreatedOn = DateTime.Now
            };

            _context.Events.Add(@event);

            return _context.SaveChanges();
        }


        public void GetEvent()
        {
            var @event = _context.Events.FirstOrDefault();
        }
    }
}
