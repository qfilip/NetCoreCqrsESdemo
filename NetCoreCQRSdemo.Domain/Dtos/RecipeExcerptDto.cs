using System;

namespace NetCoreCQRSdemo.Domain.Dtos
{
    public class RecipeExcerptDto : BaseDto
    {
        public Guid CocktailId { get; set; }
        public Guid IngredientId { get; set; }
        public int Amount { get; set; }
    }
}
