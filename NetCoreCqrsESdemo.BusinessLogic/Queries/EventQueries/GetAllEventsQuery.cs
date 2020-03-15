﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Queries.EventQueries
{
    public class GetAllEventsQuery : BaseQuery, IRequest<List<AppEventDto>>
    {
        public GetAllEventsQuery(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class GetAllEventsHandler : BaseHandler<GetAllEventsQuery, List<AppEventDto>>
    {
        public async override Task<List<AppEventDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var result = await request.context.Events
                .OrderBy(x => x.CreatedOn)
                .ToListAsync();

            return _appMapper.ToDtos(result).ToList();
        }
    }
}