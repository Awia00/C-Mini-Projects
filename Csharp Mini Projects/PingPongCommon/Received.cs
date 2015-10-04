using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCommon
{
    public struct Received
    {
        public IPEndPoint Sender { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}
