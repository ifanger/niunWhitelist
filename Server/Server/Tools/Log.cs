using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Tools
{
    class Log
    {
        public static void i(string Message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[" + DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss") + "] [INFO] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Message + '\n');
        }

        public static void e(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[" + DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss") + "] [ERRO] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Message + '\n');
        }

        public static void w(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[" + DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss") + "] [AVISO] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Message + '\n');
        }

        public static void s(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss") + "] [SUCESSO] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Message + '\n');
        }
    }
}
