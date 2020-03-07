using NetCoreCQRSdemo.Domain.Dtos;
using System.Collections.Generic;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class Cocktail : BaseEntity
    {
        public Cocktail()
        {
            Ingredients = new HashSet<Ingredient>();
        }
        public string Name { get; set; }
        public int Strength { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
