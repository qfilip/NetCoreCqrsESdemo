using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Api.Controllers.Base;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers
{
    [Route("events")]
    public class EventController : BaseController
    {

        public EventController(IMediator mediator, ApplicationDbContext context) : base(mediator, context)
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
