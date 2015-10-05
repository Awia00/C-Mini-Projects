using System;

namespace PingPongCommon.Models
{
    public class BatModel
    {
        public Point2D Point { get; set; }
        public Vector3D Normal { get; set; }
        public float Height { get; set; }

        private const float Depth = 1;

        public BatModel(Point2D point, int height, Vector3D normal)
        {
            Point = point;
            Height = height;
            Normal = normal;
        }

        public BatModel()
        {
            Point = new Point2D(0, 0);
            Normal = new Vector3D(1, 0);
            Height = 10;
        }

        public void Move(int direction)
        {
            Point = new Point2D(Point.X, Point.Y + 2 * direction);
        }

        public override string ToString()
        {
            return Point.ToString();
        }

        public bool CheckCollision(BallModel ball)
        {
            var xDiff = ball.Point.X - Point.X;
            var yDiff = ball.Point.Y - Point.Y;
            if (Math.Abs(xDiff) < Depth || Math.Abs(yDiff) < Height/2)
            {
                return true;
            }
            return false;
        }
    }
}
