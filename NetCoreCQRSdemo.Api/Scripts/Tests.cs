using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRSdemo.Api.Scripts
{
    public class Tests
    {
        private readonly ApplicationDbContext _context;
        public Tests(ApplicationDbContext context)
        {
            _context = context;
        }

        public int SeedDb()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            return 0;
        }


        public void GetEvent()
        {
            var @event = _context.Events.FirstOrDefault();
        }
    }
}
