using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Commands;
using NetCoreCqrsESdemo.BusinessLogic.Queries.IngredientQueries;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System;
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
            CommandExecutionService commandExecutionService) : base(mediator, context, commandExecutionService)
        {
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteCommands(IEnumerable<CommandInfo<IngredientDto>> commands)
        {
            var result = await _commandExecutionService.ParseAndExecute(commands);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllIngredientsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllWithIds(IEnumerable<Guid> ingredientIds)
        {
            var result = await _mediator.Send(new GetIngredientsByIds(ingredientIds));
            return Ok(result);
        }
    }
}