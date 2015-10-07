using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PingPongClient.Connections;
using PingPongCommon.DTOs;

namespace PingPongClient.ViewModels
{
    public class StartViewModel : ViewModelBase
    {
        private readonly ClientConnection _client = ClientConnection.ConnectTo("127.0.0.1", 32123);
        private int _tickInterval = 100;
        private Timer _timer;

        public StartViewModel()
        {
            GameViewModel = new GameViewModel();
            InputViewModel = new InputViewModel(_client);
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
                            var dto = JsonConvert.DeserializeObject<ObjectStateDto>(reply);
                            GameViewModel.Update(dto);
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                    }
                    catch (Exception ex)
                    {
                        reply = "Reply Failed: " + ex + "\n";
                        Debug.Write(ex);
                    }
                }
            });
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
            
            DtoBase dto;
            switch (Message)
            {
                case "play":
                    dto = new CommandDto(DtoType.Play);
                    _timer = new Timer(Tick, null, DateTime.Now.Second, _tickInterval);
                    _client.Send(JsonConvert.SerializeObject(dto));
                    break;
                case "pause":
                    dto = new CommandDto(DtoType.Pause);
                    _timer.Dispose();
                    _client.Send(JsonConvert.SerializeObject(dto));
                    break;
                case "restart":
                    dto = new CommandDto(DtoType.Restart);
                    _timer.Dispose();
                    _client.Send(JsonConvert.SerializeObject(dto));
                    break;
                default:
                    _client.Send(Message);
                    break;
            }
            Message = "";
            _sendMessageAvailable = true;
            
        }
    }
}
