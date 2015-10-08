namespace PingPongCommon
{
    public class Settings
    {
        public int UpdateTime { get; set; } = 20;

        public int UpdatesASecond
        {
            get { return 1000 / UpdateTime; }
            private set { UpdateTime = 1000 / value; }
        }
    }
}
