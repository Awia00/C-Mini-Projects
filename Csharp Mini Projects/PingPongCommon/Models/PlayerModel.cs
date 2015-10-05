namespace PingPongCommon.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public BatModel Bat { get; set; }

        public PlayerModel(BatModel bat, int id)
        {
            Bat = bat;
            Score = 0;
            Id = id;
        }

        public PlayerModel()
        {
            Bat = new BatModel();
            Score = 0;
            Id = 1;
        }
    }
}
