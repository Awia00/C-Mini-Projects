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
            Ball = new BallModel();
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
