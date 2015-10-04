using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PingPongCommon.Models;

namespace PingPongServer
{
    public class GameController
    {
        public GameModel GameModel { get; set; }
        private int _tickInterval = 41;

        private Timer _timer;
        public GameController()
        {
            GameModel = new GameModel();
        }

        private void Tick(object state)
        {
            GameModel.Ball.Move();
            Console.WriteLine(GameModel.Ball);
        }

        public void StartGame()
        {
            _timer = new Timer(Tick, null, DateTime.Now.Second, _tickInterval);
        }

        public void PauseGame()
        {
            _timer.Dispose();
        }

        public void RestartGame()
        {
            PauseGame();
            GameModel = new GameModel();
        }
    }
}
