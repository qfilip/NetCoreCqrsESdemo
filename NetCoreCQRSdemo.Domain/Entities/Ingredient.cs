using NetCoreCQRSdemo.Domain.Dtos;
using System.Collections.Generic;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public Ingredient()
        {
            Excerpts = new HashSet<RecipeExcerpt>();
        }
        public string Name { get; set; }
        public int Strength { get; set; }
        public virtual ICollection<RecipeExcerpt> Excerpts { get; set; }
    }
}
