using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Server.Classes
{
    class Client
    {
        private IPEndPoint remoteIPEndPoint;
        private bool connected = false;

        public Client(IPEndPoint remoteIPEndPoint, bool connected)
        {
            this.remoteIPEndPoint = remoteIPEndPoint;
            this.connected = connected;
        }

        public IPEndPoint RemoteIPEndPoint
        {
            get
            {
                return remoteIPEndPoint;
            }

            set
            {
                remoteIPEndPoint = value;
            }
        }

        public bool Connected
        {
            get
            {
                return connected;
            }

            set
            {
                connected = value;
            }
        }
    }
}
