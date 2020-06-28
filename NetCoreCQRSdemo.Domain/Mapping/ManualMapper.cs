using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreCQRSdemo.Domain.Mapping
{
    public class ManualMapper
    {
        public Cocktail ToEntity(CocktailDto dto)
        {
            var entity = new Cocktail
            {
                Id = dto.Id,
                Name = dto.Name,
                Excerpts = MultiMap(dto.Excerpts, ToEntity).ToList()
            };

            return entity;
        }
        public Ingredient ToEntity(IngredientDto dto)
        {
            return new Ingredient()
            {
                Id = dto.Id,
                Name = dto.Name,
                Strength = dto.Strength,
                Excerpts = MultiMap(dto.Excerpts, ToEntity).ToList()
            };
        }
        public RecipeExcerpt ToEntity(RecipeExcerptDto dto)
        {
            return new RecipeExcerpt()
            {
                Id = dto.Id,
                CocktailId = dto.CocktailId,
                IngredientId = dto.IngredientId,
                Amount = dto.Amount
            };
        }
        public AppEvent ToEntity(AppEventDto dto)
        {
            return new AppEvent()
            {
                Id = dto.Id,
                Arguments = dto.Arguments,
                CommandCode = dto.CommandCode,
                EventType = dto.EventType
            };
        }

        public CocktailDto ToDto(Cocktail entity)
        {
            var dto = new CocktailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Excerpts = MultiMap(entity.Excerpts, ToDto).ToList(),
            };

            return dto;
        }
        public IngredientDto ToDto(Ingredient entity)
        {
            var dto = new IngredientDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Strength = entity.Strength,
                Excerpts = MultiMap(entity.Excerpts, ToDto).ToList()
            };

            return dto;
        }
        public RecipeExcerptDto ToDto(RecipeExcerpt entity)
        {
            return new RecipeExcerptDto()
            {
                Id = entity.Id,
                CocktailId = entity.CocktailId,
                IngredientId = entity.IngredientId,
                Amount = entity.Amount
            };
        }
        public AppEventDto ToDto(AppEvent entity)
        {
            return new AppEventDto()
            {
                Id = entity.Id,
                Arguments = entity.Arguments,
                CommandCode = entity.CommandCode,
                EventType = entity.EventType
            };
        }

        public IEnumerable<TResult> MultiMap<TInput, TResult>(IEnumerable<TInput> inputs, Func<TInput, TResult> mapDefinition)
        {
            var resultSet = new List<TResult>();
            if(inputs != null)
            {
                foreach(var input in inputs)
                {
                    resultSet.Add(mapDefinition.Invoke(input));
                }
            }

            return resultSet;
        }
    }
}
