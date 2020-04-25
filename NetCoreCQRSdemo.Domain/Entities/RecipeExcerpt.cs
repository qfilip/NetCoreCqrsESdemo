using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class RecipeExcerpt : BaseEntity
    {
        public Guid CocktailId { get; set; }
        public Guid IngredientId { get; set; }
        public int Amount { get; set; }

        public virtual Cocktail Cocktail { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
