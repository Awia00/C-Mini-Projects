using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCommon.DTOs
{
    public class PlayerMoveDto : DtoBase
    {
        public int PlayerId { get; set; }
        public int Direction { get; set; }

        public PlayerMoveDto() : base(DtoType.Move)
        {

        }
    }
}
