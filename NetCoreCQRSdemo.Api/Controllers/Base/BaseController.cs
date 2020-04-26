using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Controllers.Base
{
    [ApiController]
    [EnableCors]
    [AllowAnonymous]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly ApplicationDbContext _context;

        public BaseController(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }
    }
}
