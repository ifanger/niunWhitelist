using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Tools
{
    class Packet
    {
        private byte[] bytes;
        private int lenght;
        private string data;

        public Packet(string data)
        {
            this.data = data;
            byte[] _data = Encoding.ASCII.GetBytes(data);
            this.Bytes = _data;
            this.Lenght = _data.Length;
        }

        public byte[] Bytes
        {
            get
            {
                return bytes;
            }

            set
            {
                bytes = value;
            }
        }

        public int Lenght
        {
            get
            {
                return lenght;
            }

            set
            {
                lenght = value;
            }
        }

        public string Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }
    }
}
