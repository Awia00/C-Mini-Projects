using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PingPongClient.Connections;
using PingPongCommon.DTOs;

namespace PingPongClient.ViewModels
{
    public class InputViewModel : ICommand
    {
        private readonly ClientConnection _client;

        public InputViewModel(ClientConnection client)
        {
            _client = client;
        }

        private bool _inputActionAvailable = true;
        private void MovePlayer1Up()
        {
            var dto = new PlayerMoveDto { Direction = 1, PlayerId = 1 };
            _client.Send(JsonConvert.SerializeObject(dto));
        }
        private void MovePlayer1Down()
        {
            var dto = new PlayerMoveDto { Direction = -1, PlayerId = 1 };
            _client.Send(JsonConvert.SerializeObject(dto));
        }
        private void MovePlayer2Up()
        {
            var dto = new PlayerMoveDto { Direction = 1, PlayerId = 2 };
            _client.Send(JsonConvert.SerializeObject(dto));
        }
        private void MovePlayer2Down()
        {
            var dto = new PlayerMoveDto { Direction = -1, PlayerId = 2 };
            _client.Send(JsonConvert.SerializeObject(dto));
        }

        public bool CanExecute(object parameter)
        {
            return _inputActionAvailable;
        }

        public void Execute(object parameter)
        {
            if (!_inputActionAvailable) return;
            _inputActionAvailable = false;
            switch (parameter.ToString())
            {
                case "W":
                    MovePlayer1Up();
                    break;
                case "S":
                    MovePlayer1Down();
                    break;
                case "Up":
                    MovePlayer2Up();
                    break;
                case "Down":
                    MovePlayer2Down();
                    break;
            }
            _inputActionAvailable = true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
