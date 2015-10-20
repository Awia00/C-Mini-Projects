using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PingPongClient.Connections;
using PingPongCommon;
using PingPongCommon.DTOs;

namespace PingPongClient.ViewModels
{
    public class StartViewModel : ViewModelBase
    {
        private readonly ClientConnection _client = ClientConnection.ConnectTo("127.0.0.1", 32123);
        private readonly Settings _settings = new Settings();
        private int _syncInterval = 80;
        private Timer _timer;
        private Timer _objectStateTimer;

        public StartViewModel()
        {
            GameViewModel = new GameViewModel();
            InputViewModel = new InputViewModel(_client, this);
            string reply = "";
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        var received = await _client.Receive();
                        reply = received.Message;
                        try
                        {
                            _objectStateTimer?.Dispose();
                            var dto = JsonConvert.DeserializeObject<ObjectStateDto>(reply);
                            MoveBallOver1Sec(dto);
                        }
                        catch (Exception ex)
                        {
                            _timer.Dispose();
                            _objectStateTimer?.Dispose();
                            History += ex;
                        }
                    }
                    catch (Exception ex)
                    {
                        reply = "Reply Failed: " + ex + "\n";
                        History += ex;
                    }
                }
            });
        }

        public void MoveBallOver1Sec(ObjectStateDto dto)
        {
            //GameViewModel.Update(dto);
            var deltaX =        (dto.BallXIn1Sec - dto.BallX) / (float)_settings.UpdatesASecond;
            var deltaY =        -((dto.BallYIn1Sec - dto.BallY) / (float)_settings.UpdatesASecond);
            var deltaBat1X =    (dto.Bat1XIn1Sec - dto.Bat1X) / (float)_settings.UpdatesASecond;
            var deltaBat1Y =    -((dto.Bat1YIn1Sec - dto.Bat1Y) / (float)_settings.UpdatesASecond);
            var deltaBat2X =    (dto.Bat2XIn1Sec - dto.Bat2X) / (float)_settings.UpdatesASecond;
            var deltaBat2Y =    -((dto.Bat2YIn1Sec - dto.Bat2Y) / (float)_settings.UpdatesASecond);
            _objectStateTimer = new Timer(state =>
            {
                dto.BallX += deltaX;
                dto.BallY += deltaY;
                dto.Bat1X += deltaBat1X;
                dto.Bat1Y += deltaBat1Y;
                dto.Bat2X += deltaBat2X;
                dto.Bat2Y += deltaBat2Y;
                GameViewModel.Update(dto);
            },null, DateTime.Now.Second, _settings.UpdatesASecond);
        }
        public void Tick(object state)
        {
            var dto = new ObjectStateDto();
            _client.Send(JsonConvert.SerializeObject(dto));
        }

        #region Properties
        private string _status;

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

        private string _history;

        public string History
        {
            get { return _history; }
            set
            {
                _history = value;
                NotifyPropertyChanged("History");
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }

        private GameViewModel _gameViewModel;
        public GameViewModel GameViewModel
        {
            get
            {
                return _gameViewModel;
            }
            set
            {
                _gameViewModel = value;
                NotifyPropertyChanged("GameViewModel");
            }
        }

        private InputViewModel _inputViewModel;
        public InputViewModel InputViewModel
        {
            get
            {
                return _inputViewModel;
            }
            set
            {
                _inputViewModel = value;
                NotifyPropertyChanged("InputViewModel");
            }
        }
        #endregion Properties


        private bool _sendMessageAvailable = true;
        public void SendMessage()
        {
            if (!_sendMessageAvailable || Message == "") return;
            _sendMessageAvailable = false;
            
            switch (Message)
            {
                default:
                    _client.Send(Message);
                    break;
            }
            Message = "";
            _sendMessageAvailable = true;
            
        }

        private bool _isPaused = true;
        public void PausePlay()
        {
            if (_isPaused)
            {
                _isPaused = false;
                var dto = new CommandDto(DtoType.Play);
                _timer = new Timer(Tick, null, DateTime.Now.Second, _syncInterval);
                _client.Send(JsonConvert.SerializeObject(dto));
            }
            else
            {
                _isPaused = true;
                var dto = new CommandDto(DtoType.Pause);
                _timer.Dispose();
                _objectStateTimer.Dispose();
                _client.Send(JsonConvert.SerializeObject(dto));
            }
            
        }

        public void Restart()
        {
            _isPaused = true;
            var dto = new CommandDto(DtoType.Restart);
            _timer.Dispose();
            _objectStateTimer.Dispose();
            _client.Send(JsonConvert.SerializeObject(dto));
        }
    }
}
