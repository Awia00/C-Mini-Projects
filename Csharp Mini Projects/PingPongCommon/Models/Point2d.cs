using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCommon.Models
{
    public class Point2D
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public Point2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "x: " + X + " y: " + Y;
        }
    }
}
