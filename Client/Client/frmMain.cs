using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Client.Tools;
using System.Net.Sockets;
using System.Net;

namespace Client
{
    public partial class frmMain : Form
    {
        Server _Server;
        bool Failed = true;

        public frmMain()
        {
            InitializeComponent();
        }

        private void lbReponseChanger(string Message, Color _Color)
        {
            lbResponse.Invoke((MethodInvoker)delegate
            {
                lbResponse.ForeColor = _Color;
                lbResponse.Text = Message;
            });
        }

        private void ErrorBox(string Msg)
        {
            lbReponseChanger(Msg, Color.DarkRed);
        }

        private void Disconnect()
        {
            txtName.Invoke((MethodInvoker)delegate
            {
                txtName.Enabled = false;
            });

            txtUID.Invoke((MethodInvoker)delegate
            {
                txtUID.Enabled = false;
            });

            btnAdd.Invoke((MethodInvoker)delegate
            {
                btnAdd.Enabled = false;
            });

            lbStatus.Invoke((MethodInvoker)delegate
            {
                lbStatus.ForeColor = Color.Red;
                lbStatus.Text = "Não foi possível conectar-se ao servidor.";
            });
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            backgroundConnect.RunWorkerAsync();
        }

        private void backgroundConnect_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _Server = new Server();
                int port;

                _Server.Address = ConfigReader.getString("ip");
                Console.WriteLine(_Server.Address);
                _Server.Password = ConfigReader.getString("key");

                Console.WriteLine(_Server.Address);

                if (!int.TryParse(ConfigReader.getString("port"), out port))
                    throw new Exception("Falha ao conectar-se com o servidor. A porta não é válida.");

                _Server.Port = port;

                _Server.UdpServer = new UdpClient();
                IPEndPoint EndPoint = new IPEndPoint(IPAddress.Parse(_Server.Address), _Server.Port);

                _Server.UdpServer.Connect(EndPoint);
                _Server.Send("LOGIN:" + _Server.Password);
            } catch (SocketException er)
            {
                Failed = true;
                ErrorBox(er.Message);
            } catch (Exception er)
            {
                Failed = true;
                ErrorBox(er.Message);
            }
        }

        private void backgroundConnect_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!Failed)
            {
                lbStatus.ForeColor = Color.DarkGreen;
                lbStatus.Text = "Conectado em " + _Server.Address + ":" + _Server.Port + ".";
            }
            backgroundResponses.RunWorkerAsync();
        }

        private void backgroundResponses_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    IPEndPoint EndPoint = new IPEndPoint(IPAddress.Parse(_Server.Address), _Server.Port);
                    byte[] receivedData = _Server.UdpServer.Receive(ref EndPoint);
                    string Data = Encoding.UTF8.GetString(receivedData);

                    if (Data == "-1")
                    {
                        lbReponseChanger("Acesso não autorizado.", Color.DarkRed);
                        Disconnect();
                    } else if (Data == "0")
                    {
                        lbReponseChanger("Acesso autorizado.", Color.DarkGreen);
                    } else if (Data == "1")
                    {
                        lbReponseChanger("O servidor não reconheceu o pacote enviado.", Color.DarkRed);
                    } else if (Data == "2")
                    {
                        lbReponseChanger("O nome enviado não é válido.", Color.DarkRed);
                    }
                    else if (Data == "3")
                    {
                        lbReponseChanger("O GUID enviado não é válido.", Color.DarkRed);
                    }
                    else if (Data == "4")
                    {
                        lbReponseChanger("Senha incorreta.", Color.DarkRed);
                        Disconnect();
                    }
                    else if (Data == "10")
                    {
                        lbReponseChanger("Usuário adicionado com sucesso.", Color.DarkGreen);
                    } else if (Data == "11")
                    {
                        lbReponseChanger("Usuário já existente na whitelist.", Color.DarkOrange);
                    } else
                    {
                        lbReponseChanger("Pacote desconhecido.", Color.DarkRed);
                    }

                    Failed = false;
                }
            }
            catch (SocketException er)
            {
                Failed = true;
                ErrorBox(er.Message);
            }
            catch (Exception er)
            {
                Failed = true;
                ErrorBox(er.Message);
            }
        }

        private void backgroundResponses_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Failed)
                Disconnect();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _Server.Send("ADD:" + A3.ValidateUsername(txtName.Text) + '\n' + A3.UIDtoGUID(txtUID.Text));
            }
            catch (Exception er)
            {
                Failed = true;
                ErrorBox(er.Message);
            }
        }
    }
}
