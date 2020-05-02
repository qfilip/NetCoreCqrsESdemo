using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Queries.IngredientQueries;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [Route("ingredients")]
    public class IngredientController : BaseController
    {
        public IngredientController(IMediator mediator, ApplicationDbContext context) : base(mediator, context)
        { }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllIngredientsAsync()
        {
            var result = await _mediator.Send(new GetAllIngredientsQuery());
            return Ok(result);
        }
    }
}