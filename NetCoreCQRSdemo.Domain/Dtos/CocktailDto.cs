using NetCoreCQRSdemo.Domain.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.Dtos
{
    public class CocktailDto : BaseDto
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }
}
