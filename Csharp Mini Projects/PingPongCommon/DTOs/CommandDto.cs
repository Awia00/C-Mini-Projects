using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCommon.DTOs
{
    public class CommandDto : DtoBase
    {
        public CommandDto(DtoType dtoType) : base(dtoType)
        {
        }
    }
}
