using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Client.Tools
{
    class Server
    {
        private string address;
        private string password;
        private int port;
        private UdpClient udpServer;

        public Server()
        {

        }

        public void Send(string data)
        {
            byte[] msg = Encoding.ASCII.GetBytes(data);
            UdpServer.Send(msg, msg.Length);
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public int Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        public UdpClient UdpServer
        {
            get
            {
                return udpServer;
            }

            set
            {
                udpServer = value;
            }
        }
    }
}
