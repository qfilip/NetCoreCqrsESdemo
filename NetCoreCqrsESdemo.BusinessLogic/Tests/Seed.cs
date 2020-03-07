using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Tests
{
    public class Seed
    {
        private readonly ApplicationDbContext _context;
        public Seed(ApplicationDbContext context)
        {
            _context = context;
        }

        public int SeedDb()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var eCount = new EventCount { CurrentCount = 0 };
            _context.EventCount.Add(eCount);

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
                    CreatedOn = DateTime.Now.ToString()
                };

                var cIngs = ings.Skip(i * 3).Take(3).ToList();
                cIngs.ForEach(x => x.CocktailId = c.Id);
                c.Ingredients = cIngs;

                _context.Cocktails.AddRange(c);
            }

            return _context.SaveChanges();
        }
    }
}
