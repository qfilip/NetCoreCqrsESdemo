using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public class CommandInfo<TDto> where TDto : BaseDto
    {
        public eCommand Type { get; set; }
        public eEventType EventType { get; set; }
        public TDto Dto { get; set; }
    }
}
