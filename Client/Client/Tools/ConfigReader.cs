using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Client.Tools
{
    class ConfigReader
    {
        private const string CONFIG_FILE = "nwl_config.txt";

        public static string getString(string Key)
        {
            if (!File.Exists(CONFIG_FILE))
                throw new Exception(string.Format("Falha ao encontrar arquivo de configuração. ({0})", CONFIG_FILE));

            StreamReader Reader = new StreamReader(CONFIG_FILE);

            string[] Lines;
            Lines = Reader.ReadToEnd().Split('\n');

            foreach(string Line in Lines)
            {
                if (string.IsNullOrEmpty(Line) || !Line.Contains("=") || Line.Trim().StartsWith("#"))
                    continue;

                int Position = Line.IndexOf('=');
                string _Key = Line.Substring(0, Position);
                string Value = Line.Substring(Position + 1);

                if (Key.Equals(_Key))
                    return Value.TrimEnd();
            }

            throw new Exception(string.Format("Não foi possível ler configuração. ({0})", Key));
        }
    }
}
