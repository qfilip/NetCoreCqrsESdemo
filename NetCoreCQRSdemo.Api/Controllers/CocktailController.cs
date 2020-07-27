using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Api.Scripts;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Queries.CocktailQueries;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [Route("cocktails")]
    public class CocktailController : BaseController
    {
        public CocktailController(IMediator mediator, ApplicationDbContext context, CommandExecutionService commandExecutionsService) : base(mediator, context, commandExecutionsService)
        {}

        [HttpGet]
        [Route("seed")]
        public IActionResult SeedDatabase()
        {
            var t = new Tests(_context);
            t.SeedDb();

            return Ok();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCocktails()
        {
            var result = await _mediator.Send(new GetAllCocktailsQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCocktail([FromBody] CocktailDto dto)
        {
            // var result = await _mediator.Send(new CreateCocktailCommand(_context, dto));
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteCocktail([FromBody] CocktailDto dto)
        {
            // var result = await _mediator.Send(new CreateCocktailCommand(_context, dto));
            return Ok();
        }
    }
}