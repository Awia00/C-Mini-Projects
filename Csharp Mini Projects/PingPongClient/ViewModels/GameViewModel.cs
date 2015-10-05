using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingPongCommon.DTOs;

namespace PingPongClient.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public GameViewModel()
        {
            
        }

        #region Properties
        private int _ballX = 100;

        public int BallX
        {
            get { return _ballX; }
            set
            {
                _ballX = value + 100;
                NotifyPropertyChanged("BallX");
            }
        }

        private int _ballY = 100;

        public int BallY
        {
            get { return _ballY; }
            set
            {
                _ballY = value + 100;
                NotifyPropertyChanged("BallY");
            }
        }


        private int _bat1X = 50;

        public int Bat1X
        {
            get { return _bat1X; }
            set
            {
                _bat1X = value + 100;
                NotifyPropertyChanged("Bat1X");
            }
        }

        private int _bat1Y = 100;

        public int Bat1Y
        {
            get { return _bat1Y; }
            set
            {
                _bat1Y = value + 100;
                NotifyPropertyChanged("Bat1Y");
            }
        }

        private int _bat2X = 150;

        public int Bat2X
        {
            get { return _bat2X; }
            set
            {
                _bat2X = value + 100;
                NotifyPropertyChanged("Bat2X");
            }
        }

        private int _bat2Y = 100;

        public int Bat2Y
        {
            get { return _bat2Y; }
            set
            {
                _bat2Y = value + 100;
                NotifyPropertyChanged("Bat2Y");
            }
        }

        #endregion Properties


        /// <summary>
        /// Takes an dto and updates the elements.
        /// </summary>
        public void Update(ObjectStateDto dto)
        {
            BallX = dto.BallX;
            BallY = -dto.BallY;
            Bat1X = dto.Bat1X;
            Bat1Y = -dto.Bat1Y;
            Bat2X = dto.Bat2X;
            Bat2Y = -dto.Bat2Y;
        }
    }
}
