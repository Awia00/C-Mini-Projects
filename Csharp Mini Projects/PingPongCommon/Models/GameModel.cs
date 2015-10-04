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
    }
}
