using System;

namespace NetCoreCQRSdemo.Domain.Dtos
{
    public class RecipeExcerptDto : BaseDto
    {
        public string CocktailId { get; set; }
        public string IngredientId { get; set; }
        public int Amount { get; set; }
    }
}
