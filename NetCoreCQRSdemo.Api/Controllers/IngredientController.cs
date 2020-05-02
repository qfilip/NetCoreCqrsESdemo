using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Commands;
using NetCoreCqrsESdemo.BusinessLogic.Queries.IngredientQueries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [Route("ingredients")]
    public class IngredientController : BaseController
    {
        public IngredientController(IMediator mediator, ApplicationDbContext context) : base(mediator, context)
        {
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllIngredientsAsync()
        {
            var result = await _mediator.Send(new GetAllIngredientsQuery());
            return Ok(result);
        }

        [Route("change")]
        [HttpPost]
        public async Task<IActionResult> ChangeIngredients([FromBody] IEnumerable<CommandPayload<IngredientDto>> payloads)
        {
            var result = await _mediator.Send(new MainCommand<IngredientDto>(payloads));
            return Ok();
        }
    }
}