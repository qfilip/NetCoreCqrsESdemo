using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreCqrsESdemo.BusinessLogic.Commands
{
    public class CreateCocktail : BaseCommand
    {
        private CocktailDto _dto;
        public CreateCocktail(ApplicationDbContext context, CocktailDto dto) : base(context)
        {
            _dto = dto;
        }

        public override void Handle()
        {
            var entity = _mapper.ToDto(_dto);
        }

        public override void Reinvoke(string args)
        {
            _dto = JsonConvert.DeserializeObject<CocktailDto>(args);
            Handle();
        }

        public override string SerializeArguments()
        {
            return JsonConvert.SerializeObject(_dto);
        }


    }
}
