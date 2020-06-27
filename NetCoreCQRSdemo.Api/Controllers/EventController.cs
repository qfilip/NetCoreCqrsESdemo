using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [Route("events")]
    public class EventController : BaseController
    {

        public EventController(IMediator mediator, ApplicationDbContext context, CommandPayloadService commandPayloadService) : base(mediator, context, commandPayloadService)
        {}

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            // var result = await _mediator.Send(new GetAllEventsQuery(_context));
            return Ok();
        }

        [HttpPost]
        [Route("clear")]
        public async Task<IActionResult> ClearEvents()
        {
            // var result = await _mediator.Send(new SetInitialStateCommand(_context));
            return Ok();
        }
    }
}
