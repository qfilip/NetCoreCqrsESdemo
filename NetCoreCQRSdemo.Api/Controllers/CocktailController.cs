using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Queries;
using NetCoreCqrsESdemo.BusinessLogic.Tests;
using System;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("cocktails")]
    public class CocktailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        private readonly Seed _seed;

        public CocktailController(IMediator mediator, ApplicationDbContext context, Seed seed)
        {
            _mediator = mediator;
            _context = context;
            _seed = seed;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCocktailsAsync()
        {
            var result = await _mediator.Send(new GetAllCocktailsQuery(_context));
            return Ok(result);
        }
    }
}
