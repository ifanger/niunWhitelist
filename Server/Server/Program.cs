using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Diagnostics;

using Server.Classes;
using Server.Tools;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int Port;
                string Data = "";
                bool Debug = false;

                Console.Title = "nWL - Server";
                Log.i("Tentando inicializar ferramenta...");

                string Key = ConfigReader.getString("key");
                string configFile = ConfigReader.getString("file");
                string batFile = ConfigReader.getString("bat");

                if (Key.Equals("PLEASE_CHANGE_ME_123"))
                    throw new Exception("O servidor não pode ser iniciado com a senha padrão. (verifique o arquivo de configuração)");

                if (!int.TryParse(ConfigReader.getString("port"), out Port))
                    throw new Exception("A porta escolhida não é válida.");

                if (!File.Exists(configFile))
                    throw new Exception(string.Format("O arquivo de whitelist especificado não existe.\nArquivo: {0}", configFile));

                if (!File.Exists(batFile))
                    throw new Exception(string.Format("O arquivo bat especificado não existe.\nArquivo: {0}", batFile));

                Log.s("Arquivo de configuração lido com sucesso!");

                UdpClient Server = new UdpClient(Port);
                IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                List<Client> Clients = new List<Client>();

                Log.s(string.Format("Servidor ligado na porta {0}.", Port));
                while (true)
                {
                    byte[] receivedBytes = Server.Receive(ref remoteIPEndPoint);
                    int index = -1;
                    Data = Encoding.ASCII.GetString(receivedBytes);

                    if (Debug)
                        Log.i("Mensagem recebida: " + Data.TrimEnd());

                    /*
                     * -1 = acesso não autorizado
                     * 0 = indefinido
                     * 1 = pacote estranho
                     * 2 = nome inválido
                     * 3 = uid inválido
                     * 4 = senha incorreta
                     * 10 = adicionado
                    */
                    string Response = "0";

                    for(int i = 0; i < Clients.Count; i++)
                        if (Clients[i].RemoteIPEndPoint.ToString() == remoteIPEndPoint.ToString())
                            index = i;

                    if (index == -1)
                    {
                        Log.w("Nova conexão de usuário.");
                        Client Client = new Client(remoteIPEndPoint, false);
                        Clients.Add(Client);
                        index = (Clients.Count - 1);
                    }

                    if (Data.StartsWith("ADD:") && Data.Length > 4 && Clients[index].Connected)
                    {
                        string Packet = Data.Substring(4);
                        if (!Packet.Contains('\n'))
                        {
                            Log.e("Pacote inválido recebido de " + Clients[index].RemoteIPEndPoint);
                            Response = "1";
                        }

                        User User = new Classes.User();
                        User.Name = Packet.Split('\n')[0];
                        User.UID = Packet.Split('\n')[1];

                        if (User.Name.Length > 20)
                        {
                            Log.e("Nome de usuário inválido recebido de " + Clients[index].RemoteIPEndPoint);
                            Response = "2";
                        }

                        if (User.UID.Length != 32)
                        {
                            Log.e("GUID inválida recebida de " + Clients[index].RemoteIPEndPoint);
                            Response = "3";
                        }

                        if (Response == "0")
                        {
                            using (StreamReader Reader = new StreamReader(configFile))
                            {
                                string players = Reader.ReadToEnd();
                                Reader.Close();

                                if (!players.Contains(User.UID))
                                {
                                    using (StreamWriter Writer = File.AppendText(configFile))
                                    {
                                        Writer.Write(string.Format("\n{0} {1}", User.UID, User.Name));
                                        Writer.Close();

                                        Process.Start(batFile);

                                        Response = "10";
                                        Log.s(string.Format("Usuário: {0} | UID: {1}", User.Name, User.UID));
                                    }
                                } else
                                {
                                    Response = "11";
                                    Log.w("Tentativa de adicionar usuário já existente: " + User.Name);
                                }
                            }

                        }
                    }
                    else if (Data.StartsWith("LOGIN:") && Data.Length > 6 && !Clients[index].Connected)
                    {
                        string Packet = Data.Substring(6);

                        if (!Packet.Equals(Key))
                        {
                            Response = "4";
                            Log.e("Senha inválida recebida de " + Clients[index].RemoteIPEndPoint);
                            Log.e(Packet);
                        }

                        if (Response == "0")
                        {
                            Clients[index].Connected = true;
                            Log.s("Usuário conectado " + Clients[index].RemoteIPEndPoint);
                        }
                    } else
                    {
                        Log.e("Cliente não conectado corretamente " + Clients[index].RemoteIPEndPoint + " " + Clients[index].Connected);

                        foreach(Client _Client in Clients)
                        {
                            Log.e(_Client.RemoteIPEndPoint + " " + _Client.Connected);
                        }

                        Response = "-1";
                    }

                    string Received = Data.TrimEnd();

                    byte[] _Response = Encoding.ASCII.GetBytes(Response);
                    Server.Send(_Response, _Response.Length, remoteIPEndPoint);

                    if (Debug)
                        Log.i("Mensagem enviada para " + remoteIPEndPoint + ": " + Encoding.ASCII.GetString(_Response));
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERRO CRÍTICO: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}