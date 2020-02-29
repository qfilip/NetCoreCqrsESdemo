using NetCoreCQRSdemo.Domain.DomainBase;
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
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
