using NetCoreCQRSdemo.Domain.Dtos;

namespace NetCoreCQRSdemo.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string CocktailId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string UnitOfMeasure { get; set; }

        public virtual Cocktail Cocktail { get; set; }
    }
}
