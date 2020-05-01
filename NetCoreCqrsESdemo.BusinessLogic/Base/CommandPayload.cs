using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public class CommandPayload<TDto> where TDto : BaseDto
    {
        public eCommand CommandType { get; set; }
        public TDto Payload { get; set; }
    }
}
