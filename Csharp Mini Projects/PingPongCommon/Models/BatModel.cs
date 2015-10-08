using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingPongCommon.Models
{
    public class BatModel
    {
        public Point2D Point { get; set; }
        public Point2D PosInOneSec { get; private set; }
        public Vector3D Normal { get; set; }
        public float SpeedPerSecond { get; set; }
        public float Height { get; set; }

        private const float Depth = 10;

        public BatModel(Point2D point, float height, Vector3D normal)
        {
            Point = point;
            PosInOneSec = Point;
            Height = height;
            Normal = normal;
            SpeedPerSecond = 60;
        }

        public BatModel()
        {
            Point = new Point2D(0, 0);
            PosInOneSec = Point;
            Normal = new Vector3D(1, 0);
            Height = 10;
            SpeedPerSecond = 60;
        }

        private Task _moveTask;
        public async void Move(int direction, int fps)
        {
            _moveTask?.Dispose();
            PosInOneSec = new Point2D(Point.X, Point.Y + SpeedPerSecond * direction);
            _moveTask = Task.Run(() =>
            {
                int i = 1;
                while (i <= fps/4)
                {
                    Point = new Point2D(Point.X, Point.Y + (SpeedPerSecond/fps) * direction);
                    Console.WriteLine("Bat moved to" + Point);
                    Thread.Sleep(1000/fps);
                    i++;
                }
            });
            await _moveTask;
            PosInOneSec = Point;
        }

        public override string ToString()
        {
            return Point.ToString();
        }

        public bool CheckCollision(BallModel ball)
        {
            var xDiff = ball.Point.X - Point.X;
            var yDiff = ball.Point.Y - Point.Y;
            if (Math.Abs(xDiff) < Depth / 2 + ball.Radius && Math.Abs(yDiff) < Height / 2 + ball.Radius)
            {
                return true;
            }
            return false;
        }
    }
}
