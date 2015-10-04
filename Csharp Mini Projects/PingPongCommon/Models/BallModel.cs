namespace PingPongCommon.Models
{
    public class BallModel
    {
        public Point2D Point { get; set; }
        public Vector3D Direction { get; set; }
        public int Radius { get; set; }


        public BallModel(Point2D point, Vector3D direction, int radius)
        {
            Point = point;
            Radius = radius;
            Direction = direction;
        }

        public BallModel()
        {
            Point = new Point2D(0,0);
            Direction = new Vector3D(1,1);
            Radius = 1;
        }

        public void Move()
        {
            Point = new Point2D(Point.X + Direction.X, Point.Y + Direction.Y);
        }


        public override string ToString()
        {
            return Point.ToString();
        }
    }
}
