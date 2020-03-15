using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreCQRSdemo.Domain.Mapping
{
    public class AppMapper
    {
        #region TO_ENTITIES
        public Cocktail ToEntity(CocktailDto dto)
        {
            var entity = new Cocktail
            {
                Id = dto.Id,
                Name = dto.Name,
                Strength = dto.Strength,
                Ingredients = ToEntities(dto.Ingredients).ToList()
            };

            return entity;
        }
        public Ingredient ToEntity(IngredientDto dto)
        {
            return new Ingredient()
            {
                Id = dto.Id,
                CocktailId = dto.CocktailId,
                Amount = dto.Amount,
                Name = dto.Name,
                UnitOfMeasure = dto.UnitOfMeasure
            };
        }
        
        public IEnumerable<Cocktail> ToEntities(IEnumerable<CocktailDto> dtos)
        {
            var entites = new List<Cocktail>();
            foreach (var dto in dtos)
            {
                var entity = ToEntity(dto);
                entites.Add(entity);
            }

            return entites;
        }
        public IEnumerable<Ingredient> ToEntities(IEnumerable<IngredientDto> dtos)
        {
            var entites = new List<Ingredient>();
            foreach (var dto in dtos)
            {
                var entity = ToEntity(dto);
                entites.Add(entity);
            }

            return entites;
        }
        #endregion

        #region TO_DTOS
        public CocktailDto ToDto(Cocktail cocktail)
        {
            var dto = new CocktailDto
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Strength = cocktail.Strength,
                Ingredients = ToDtos(cocktail.Ingredients).ToList(),
            };

            return dto;
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
        public AppEventDto ToDto(AppEvent appEvent)
        {
            return new AppEventDto()
            {
                Id = appEvent.Id,
                Arguments = appEvent.Arguments,
                CommandCode = appEvent.CommandCode
            };
        }
        
        public IEnumerable<CocktailDto> ToDtos(IEnumerable<Cocktail> cocktails)
        {
            var dtos = new List<CocktailDto>();
            foreach (var cocktail in cocktails)
            {
                dtos.Add(ToDto(cocktail));
            }

            return dtos;
        }
        public IEnumerable<IngredientDto> ToDtos(IEnumerable<Ingredient> ingredients)
        {
            var dtos = new List<IngredientDto>();
            foreach(var ingredient in ingredients)
            {
                dtos.Add(ToDto(ingredient));
            }

            return dtos;
        }
        public IEnumerable<AppEventDto> ToDtos(IEnumerable<AppEvent> appEvents)
        {
            var dtos = new List<AppEventDto>();
            foreach(var appEvent in appEvents)
            {
                var dto = ToDto(appEvent);
                dtos.Add(dto);
            }
            
            return dtos;
        }
        #endregion
    }
}
