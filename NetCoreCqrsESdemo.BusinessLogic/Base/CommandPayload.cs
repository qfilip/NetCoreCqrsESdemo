using NetCoreCQRSdemo.Domain.Dtos;
using NetCoreCQRSdemo.Domain.Enumerations;

namespace NetCoreCqrsESdemo.BusinessLogic.Base
{
    public class CommandInfo<TDto> where TDto : BaseDto
    {
        public eCommand Command { get; set; }
        public eCommandType CommandType { get; set; }
        public TDto Dto { get; set; }
    }
}