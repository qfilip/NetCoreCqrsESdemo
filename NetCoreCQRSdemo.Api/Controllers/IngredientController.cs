using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Commands;
using NetCoreCqrsESdemo.BusinessLogic.Queries.IngredientQueries;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [Route("ingredients")]
    public class IngredientController : BaseController
    {
        public IngredientController(
            IMediator mediator,
            ApplicationDbContext context,
            CommandPayloadService commandPayloadService) : base(mediator, context, commandPayloadService)
        {
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllIngredientsAsync()
        {
            var result = await _mediator.Send(new GetAllIngredientsQuery());
            return Ok(result);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateIngredients([FromBody] IEnumerable<CommandPayload<IngredientDto>> payloads)
        {
            await _commandPayloadService.ParseAndSendPayload(payloads);
            return Ok();
        }

        [Route("change")]
        [HttpPost]
        public async Task<IActionResult> ChangeIngredients([FromBody] IEnumerable<CommandPayload<IngredientDto>> payloads)
        {
            await _commandPayloadService.ParseAndSendPayload(payloads);
            return Ok();
        }
    }
}