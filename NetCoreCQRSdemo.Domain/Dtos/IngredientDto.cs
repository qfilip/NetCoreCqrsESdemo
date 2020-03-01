﻿using NetCoreCQRSdemo.Domain.DomainBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCQRSdemo.Domain.Dtos
{
    public class IngredientDto : BaseDto
    {
        public string CocktailId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}
