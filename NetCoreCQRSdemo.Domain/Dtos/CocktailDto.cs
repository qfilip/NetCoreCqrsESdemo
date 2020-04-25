using System.Collections.Generic;

namespace NetCoreCQRSdemo.Domain.Dtos
{
    public class CocktailDto : BaseDto
    {
        public string Name { get; set; }
        public List<RecipeExcerptDto> Excerpts { get; set; }
    }
}
