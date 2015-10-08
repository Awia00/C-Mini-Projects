using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCommon.DTOs
{
    public class ObjectStateDto : DtoBase
    {
        public float BallX { get; set; }
        public float BallY { get; set; }
        public float BallXIn1Sec { get; set; }
        public float BallYIn1Sec { get; set; }
        public float Bat1X { get; set; }
        public float Bat1Y { get; set; }
        public float Bat1XIn1Sec { get; set; }
        public float Bat1YIn1Sec { get; set; }
        public float Bat2X { get; set; }
        public float Bat2Y { get; set; }
        public float Bat2XIn1Sec { get; set; }
        public float Bat2YIn1Sec { get; set; }
        public ObjectStateDto() : base(DtoType.ObjectState)
        {
        }
    }
}
