using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PingPongCommon.DTOs;
using PingPongCommon.Models;

namespace PingPongServer
{
    public class GameController
    {
        public GameModel GameModel { get; set; }
        public Settings Settings { get; set; }
        private bool _isPaused = true;

        private Timer _timer;
        public GameController()
        {
            GameModel = new GameModel();
            Settings = new Settings();
        }

        private void Tick(object state)
        {
            GameModel.CheckAndHandleCollision();
            GameModel.Ball.Move();
            Console.WriteLine(GameModel.Ball);
        }

        public void StartGame()
        {
            _isPaused = false;
            _timer = new Timer(Tick, null, DateTime.Now.Second, Settings.UpdatesASecond);
        }

        public void PauseGame()
        {
            _isPaused = true;
            _timer.Dispose();
        }

        public void RestartGame()
        {
            PauseGame();
            GameModel = new GameModel();
        }

        public ObjectStateDto GetObjectStateDto()
        {
            return new ObjectStateDto
            {
                BallX = (int)GameModel.Ball.Point.X, 
                BallY = (int)GameModel.Ball.Point.Y,
                Bat1X = (int)GameModel.Player1.Bat.Point.X,
                Bat1Y = (int)GameModel.Player1.Bat.Point.Y,
                Bat2X = (int)GameModel.Player2.Bat.Point.X,
                Bat2Y = (int)GameModel.Player2.Bat.Point.Y,
            };
        }

        /// <summary>
        /// Move Player with id== playerId either up or down depending on the direction parameter.
        /// </summary>
        /// <param name="playerId">id of the player</param>
        /// <param name="direction">1 for up -1 for down.</param>
        public void MovePlayer(int playerId, int direction)
        {
            if (_isPaused) return;
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
