using MediatR;
using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Entities;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public abstract class BaseCommand
    {
        private readonly ApplicationDbContext _context;

        public BaseCommand(ApplicationDbContext context)
        {
            _context = context;
        }

        public abstract void Handle();
        public abstract string SerializeArguments();
        public abstract void Reinvoke(string args);
        protected async Task LogEvent<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            var counter = await _context.EventCount.FirstOrDefaultAsync();

            var @event = new AppEvent()
            {
                Arguments = command.SerializeArguments(),
                CommandCode = (typeof(TCommand)).GetHashCode(),
                OrderNumber = counter.CurrentCount++
            };

            counter.CurrentCount += 1;
            await _context.Events.AddAsync(@event);

            await _context.SaveChangesAsync();
        }
    }
}
