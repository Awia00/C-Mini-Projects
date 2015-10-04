namespace PingPongCommon.Models
{
    public class BatModel
    {
        public Point2D Point { get; set; }
        public int Width { get; set; }

        public BatModel(Point2D point, int width)
        {
            Point = point;
            Width = width;
        }

        public BatModel()
        {
            Point = new Point2D(0, 0);
            Width = 10;
        }
    }
}
