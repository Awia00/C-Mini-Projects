﻿namespace PingPongCommon.Models
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
            Ball = new BallModel(new Point2D(150, -100), new Vector3D(-30, 1f), 1);
            Player1 = new PlayerModel(new BatModel(new Point2D(100, -100), 50, new Vector3D(1, 0)), 1);
            Player2 = new PlayerModel(new BatModel(new Point2D(200, -100), 50, new Vector3D(-1, 0)), 2);
        }

        public void CheckAndHandleCollision()
        {
            if (Player1.Bat.CheckCollision(Ball))
            {
                Ball.Direction = Vector3D.ReflectionVector3D(Ball.Direction, Player1.Bat.Normal);
                Ball.Direction = Ball.Direction.VectorTimesFactor(1.2f);
            }
            else if (Player2.Bat.CheckCollision(Ball))
            {
                Ball.Direction = Vector3D.ReflectionVector3D(Ball.Direction, Player2.Bat.Normal);
                Ball.Direction = Ball.Direction.VectorTimesFactor(1.2f);
            }
        }
    }
}
