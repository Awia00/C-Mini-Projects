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
            GameModel.CheckAndHandleCollision();
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

        /// <summary>
        /// Move Player with id== playerId either up or down depending on the direction parameter.
        /// </summary>
        /// <param name="playerId">id of the player</param>
        /// <param name="direction">1 for up -1 for down.</param>
        public void MovePlayer(int playerId, int direction)
        {
            if (playerId == GameModel.Player1.Id)
            {
                GameModel.Player1.Bat.Move(direction/Math.Abs(direction)); // Math.abs to ensure only 1 and -1 is used.
            }
            else if (playerId == GameModel.Player2.Id)
            {
                GameModel.Player2.Bat.Move(direction / Math.Abs(direction)); // Math.abs to ensure only 1 and -1 is used.
            }
        }
    }
}
