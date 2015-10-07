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
        private int _ballX;

        public int BallX
        {
            get { return _ballX; }
            set
            {
                _ballX = value;
                NotifyPropertyChanged("BallX");
            }
        }

        private int _ballY;

        public int BallY
        {
            get { return _ballY; }
            set
            {
                _ballY = value;
                NotifyPropertyChanged("BallY");
            }
        }


        private int _bat1X;

        public int Bat1X
        {
            get { return _bat1X; }
            set
            {
                _bat1X = value;
                NotifyPropertyChanged("Bat1X");
            }
        }

        private int _bat1Y;

        public int Bat1Y
        {
            get { return _bat1Y; }
            set
            {
                _bat1Y = value;
                NotifyPropertyChanged("Bat1Y");
            }
        }

        private int _bat2X;

        public int Bat2X
        {
            get { return _bat2X; }
            set
            {
                _bat2X = value;
                NotifyPropertyChanged("Bat2X");
            }
        }

        private int _bat2Y;

        public int Bat2Y
        {
            get { return _bat2Y; }
            set
            {
                _bat2Y = value;
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
