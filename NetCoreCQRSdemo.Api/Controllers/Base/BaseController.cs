﻿using MediatR;
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
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly ApplicationDbContext _context;
        protected readonly CommandExecutionService _commandExecutionService;

        public BaseController(IMediator mediator, ApplicationDbContext context, CommandExecutionService commandExecutionService)
        {
            _mediator = mediator;
            _context = context;
            _commandExecutionService = commandExecutionService;
        }
    }
}
