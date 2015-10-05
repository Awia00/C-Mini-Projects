namespace PingPongCommon.Models
{
    public class GameModel
    {
        public PlayerModel Player1 { get; set; }
        public PlayerModel Player2 { get; set; }
        public BallModel Ball { get; set; }

        public GameModel(PlayerModel player1, PlayerModel player2, BallModel ball)
        {
            Player1 = player1;
            Player2 = player2;
            Ball = ball;
        }

        public GameModel()
        {
            Ball = new BallModel(new Point2D(0, 0), new Vector3D(-3,0), 1);
            Player1 = new PlayerModel(new BatModel(new Point2D(-50, 0), 50, new Vector3D(1, 0)), 1);
            Player2 = new PlayerModel(new BatModel(new Point2D(50, 0), 50, new Vector3D(-1, 0)), 2);
        }

        public void CheckAndHandleCollision()
        {
            if (Player1.Bat.CheckCollision(Ball))
            {
                Ball.Direction = Vector3D.ReflectionVector3D(Ball.Direction, Player1.Bat.Normal);
            }
            else if (Player2.Bat.CheckCollision(Ball))
            {
                Ball.Direction = Vector3D.ReflectionVector3D(Ball.Direction, Player2.Bat.Normal);
            }
        }
    }
}
