using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Services;
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
        protected readonly CommandPayloadService _commandPayloadService;

        public BaseController(IMediator mediator, ApplicationDbContext context, CommandPayloadService commandPayloadService)
        {
            _mediator = mediator;
            _context = context;
            _commandPayloadService = commandPayloadService;
        }
    }
}
