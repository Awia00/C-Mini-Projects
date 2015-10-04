namespace PingPongCommon.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public BatModel Bat { get; set; }

        public PlayerModel(BatModel bat)
        {
            Bat = bat;
            Score = 0;
        }

        public PlayerModel()
        {
            Bat = new BatModel();
            Score = 0;
        }
    }
}
