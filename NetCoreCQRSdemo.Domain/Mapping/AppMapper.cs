using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using System.Collections.Generic;

namespace NetCoreCQRSdemo.Domain.Mapping
{
    public class AppMapper
    {
        public CocktailDto ToDto(Cocktail cocktail)
        {
            var dto = new CocktailDto
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Strength = cocktail.Strength,
                Ingredients = ToDto(cocktail.Ingredients),
            };

            return dto;
        }

        public List<CocktailDto> ToDtos(List<Cocktail> cocktails)
        {
            var dtos = new List<CocktailDto>();
            foreach (var cocktail in cocktails)
            {
                dtos.Add(ToDto(cocktail));
            }

            return dtos;
        }

        public IngredientDto ToDto(Ingredient ingredient)
        {
            var dto = new IngredientDto
            {
                Id = ingredient.Id,
                CocktailId = ingredient.CocktailId,
                Name = ingredient.Name,
                Amount = ingredient.Amount,
                UnitOfMeasure = ingredient.UnitOfMeasure
            };

            return dto;
        }

        private List<IngredientDto> ToDto(ICollection<Ingredient> ingredients)
        {
            var dtos = new List<IngredientDto>();
            foreach(var ingredient in ingredients)
            {
                dtos.Add(ToDto(ingredient));
            }

            return dtos;
        }
    }
}
