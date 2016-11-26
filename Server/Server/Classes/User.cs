using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Classes
{
    class User
    {
        private string uID;
        private string name;

        public string UID
        {
            get
            {
                return uID;
            }

            set
            {
                uID = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public User()
        {
        }

       
    }
}
