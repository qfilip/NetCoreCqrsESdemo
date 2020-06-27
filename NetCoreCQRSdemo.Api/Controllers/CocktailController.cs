using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Api.Scripts;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Commands;
using NetCoreCqrsESdemo.BusinessLogic.Queries;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [Route("cocktails")]
    public class CocktailController : BaseController
    {
        public CocktailController(IMediator mediator, ApplicationDbContext context, CommandPayloadService commandPayloadService) : base(mediator, context, commandPayloadService)
        {}

        [HttpGet]
        [Route("seed")]
        public void SeedDatabase()
        {
            var t = new Tests(_context);
            t.SeedDb();
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCocktailsAsync()
        {
            // var result = await _mediator.Send(new GetAllCocktailsQuery(_context));
            return Ok();
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