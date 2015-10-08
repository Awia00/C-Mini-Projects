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
        private float _ballX;

        public int BallX
        {
            get { return (int)_ballX; }
            set
            {
                _ballX = value;
                NotifyPropertyChanged("BallX");
            }
        }

        private float _ballY;

        public int BallY
        {
            get { return (int)_ballY; }
            set
            {
                _ballY = value;
                NotifyPropertyChanged("BallY");
            }
        }


        private float _bat1X;

        public int Bat1X
        {
            get { return (int)_bat1X; }
            set
            {
                _bat1X = value;
                NotifyPropertyChanged("Bat1X");
            }
        }

        private float _bat1Y;

        public int Bat1Y
        {
            get { return (int)_bat1Y; }
            set
            {
                _bat1Y = value;
                NotifyPropertyChanged("Bat1Y");
            }
        }

        private float _bat2X;

        public int Bat2X
        {
            get { return (int)_bat2X; }
            set
            {
                _bat2X = value;
                NotifyPropertyChanged("Bat2X");
            }
        }

        private float _bat2Y;

        public int Bat2Y
        {
            get { return (int)_bat2Y; }
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
            _ballX = dto.BallX;
            _ballY = -dto.BallY;
            _bat1X = dto.Bat1X;
            _bat1Y = -dto.Bat1Y;
            _bat2X = dto.Bat2X;
            _bat2Y = -dto.Bat2Y;
            NotifyPropertyChanged("");
        }
    }
}
