using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCommon.DTOs
{
    public class ObjectStateDto : DtoBase
    {
        public int BallX { get; set; }
        public int BallY { get; set; }
        public int Bat1X { get; set; }
        public int Bat1Y { get; set; }
        public int Bat2X { get; set; }
        public int Bat2Y { get; set; }
        public ObjectStateDto() : base(DtoType.ObjectState)
        {
        }
    }
}
