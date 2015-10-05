using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PingPongClient.Connections;
using PingPongCommon.DTOs;

namespace PingPongClient.ViewModels
{
    public class StartViewModel : ViewModelBase
    {
        public StartViewModel()
        {

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
        #endregion Properties


        private bool _sendMessageAvailable = true;
        public async void SendMessage()
        {
            if (!_sendMessageAvailable || Message == "") return;
            _sendMessageAvailable = false;

            Status = "Sending message...";
            History += Message + "\n";
            
            //create a new client
            var client = ClientConnection.ConnectTo("127.0.0.1", 32123);

            string reply = "";
            //wait for reply messages from server and send them to console 
            var task = Task.Run(async () =>
            {
                try
                {
                    var received = await client.Receive();
                    reply = received.Message;
                }
                catch (Exception ex)
                {
                    reply = "Reply Failed: " + ex + "\n";
                    Debug.Write(ex);
                }
            });
            DtoBase dto;
            switch (Message)
            {
                case "play":
                    dto = new CommandDto(DtoType.Play);
                    client.Send(JsonConvert.SerializeObject(dto));
                    break;
                case "pause":
                    dto = new CommandDto(DtoType.Pause);
                    client.Send(JsonConvert.SerializeObject(dto));
                    break;
                case "restart":
                    dto = new CommandDto(DtoType.Restart);
                    client.Send(JsonConvert.SerializeObject(dto));
                    break;
                default:
                    client.Send(Message);
                    break;
            }
            client.Send(JsonConvert.SerializeObject(Message));
            await task;
            History += "Reply: " + reply +"\n";
            Message = "";
            Status = "Done";
            _sendMessageAvailable = true;
        }
    }
}
