using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Scripts;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Commands;
using NetCoreCqrsESdemo.BusinessLogic.Queries;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [ApiController]
    [EnableCors]
    [AllowAnonymous]
    [Route("cocktails")]
    public class CocktailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public CocktailController(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        [Route("seed")]
        public void SeedDatabase()
        {
            var t = new Tests(_context);
            t.GetEvent();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCocktailsAsync()
        {
            var result = await _mediator.Send(new GetAllCocktailsQuery(_context));
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCocktail([FromBody] CocktailDto dto)
        {
            var result = await _mediator.Send(new CreateCocktailCommand(_context, dto));
            return Ok(result);
        }
    }
}
