using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Api.Scripts;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Commands.CocktailCommands;
using NetCoreCqrsESdemo.BusinessLogic.Queries.CocktailQueries;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    public class CocktailController : BaseController
    {
        public CocktailController(
            IMediator mediator,
            ApplicationDbContext context,
            CommandExecutionService commandExecutionService) : base(mediator, context, commandExecutionService)
        {
        }

        [HttpGet]
        public IActionResult SeedDatabase()
        {
            var t = new Tests(_context);
            t.SeedDb();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteCommands(IEnumerable<CommandInfo<CocktailDto>> commands)
        {
            var result = await _commandExecutionService.ParseAndExecute(commands);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCocktailsQuery());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEditData()
        {
            var result = await _mediator.Send(new GetAllCocktailsQuery());
            return Ok(result);
        }
    }
}