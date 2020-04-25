using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class RecipeExcerpt : BaseEntity
    {
        public string CocktailId { get; set; }
        public string IngredientId { get; set; }
        public int Amount { get; set; }

        public virtual Cocktail Cocktail { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
