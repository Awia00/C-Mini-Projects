using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongServer
{
    public class Settings
    {
        private int _updatesASecond = 50;
        public int UpdatesASecond
        {
            get { return _updatesASecond; }
            set { _updatesASecond = 1000/value; }
        }
    }
}
