using NetCoreCQRSdemo.Domain.Dtos;
using System.Collections.Generic;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class Cocktail : BaseEntity
    {
        public Cocktail()
        {
            Excerpts = new HashSet<RecipeExcerpt>();
        }
        public string Name { get; set; }
        public virtual ICollection<RecipeExcerpt> Excerpts { get; set; }
    }
}
