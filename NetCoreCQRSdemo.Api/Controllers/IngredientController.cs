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
using System.Linq;
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

        [HttpPost]
        [Route("action")]
        public async Task<IActionResult> ExecuteCommands(IEnumerable<CommandPayload<IngredientDto>> commands)
        {
            var result = await _commandPayloadService.ParseAndSendPayload(commands);
            return Ok(result);
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllIngredientsAsync()
        {
            var result = await _mediator.Send(new GetAllIngredientsQuery());
            return Ok(result);
        }
    }
}