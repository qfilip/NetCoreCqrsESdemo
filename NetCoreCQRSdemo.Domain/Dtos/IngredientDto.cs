using System.Collections.Generic;

namespace NetCoreCQRSdemo.Domain.Dtos
{
    public class IngredientDto : BaseDto
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public List<RecipeExcerptDto> Excerpts { get; set; }
    }
}
