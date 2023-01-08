using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Policy;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public partial class Cliente : Form
    {
        Socket server;
        Thread atender;
        delegate void DelegadoParaBorrar();
        delegate void DelegadoParaEscribirList(string conectados);
        delegate void DelegadoParaLimpiarList();
        delegate void DelegadoParaDesactLogIn();
        delegate void DelegadoParaActLogIn();
        delegate void DelegadoParaActDesconectar();
        delegate void DelegadoParaDesactDesconectar();
        delegate void DelegadoParaEscribirInv(string username);
        delegate void DelegadoParaActAccept();
        delegate void DelegadoParaDesactAccept();
        delegate void DelegadoParaActDeny();
        delegate void DelegadoParaDesactDeny();
        delegate void DelegadoParaActInv();
        delegate void DelegadoParaDesactInv();
        delegate void DelegadoParaListBox(string msg);
        delegate void DelegadoParaEscribirSuperU(string username);
        delegate void DelegadoParaBorrarSuperU();
        delegate void DelegadoParaBorrarChat();
        delegate void DelegadoParaDesactRegistrar();
        delegate void DelegadoParaActRegistrar();
        delegate void DelegadoParaActDown();
        delegate void DelegadoParaDesactDown();
        delegate void DelegadoParaBorrarTBChat();
        delegate void DelegadoParaActEnv();
        delegate void DelegadoParaDesactEnv();
        delegate void DelegadoParaBGGray();
        delegate void DelegadoParaBorrarInv();
        int puerto = 9060;
        string usuario;
        string invitado;
        public Cliente()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }
        public void AddItem(string msg) 
        {
            listBoxChat.Items.Add(msg);
        }
        public void BorraTextBoxRegistrar()
        {
            textBoxName.Text = "";
            textBoxUserLog.Text = "";
            textBoxPasswordLog1.Text = "";
            textBoxPasswordLog2.Text = "";
        }
        public void BorraTextBoxIniciar()
        {
            textBoxUsername.Text = "";
            textBoxPassword.Text = "";
        }
        public void AddRowList(string trozo)
        {
            int rowID = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[rowID];
            row.Cells[0].Value = trozo;
            
        }
        public void LoginDesact()
        {
            buttonLogIn.Enabled = false;
        }
        public void LoginAct()
        {
            buttonLogIn.Enabled = true;
        }
        public void DesconectAct()
        {
            buttonDesconectar.Enabled = true;  
        }
        public void DesconectDesact()
        {
            buttonDesconectar.Enabled = false;
        }
        public void ClearList()
        {
            dataGridView1.Rows.Clear();
        }
        public void EscribirInv(string username)
        {
            labelInv.Visible = true;
            labelInv.Text = username + " te ha invitado a una partida";
        }
        public void BorrarInv()
        {
            labelInv.Text = "";
        }
        public void ActAccept()
        {
            buttonAccept.Enabled = true ;
        }
        public void DesactAccept()
        {
            buttonAccept.Enabled = false;
        }
        public void ActDeny()
        {
            buttonDeny.Enabled = true;
        }
        public void DesactDeny()
        {
            buttonDeny.Enabled = false;
        }
        public void ActInv()
        {
            buttonInv.Enabled = true;
        }
        public void DesactInv()
        {
            buttonInv.Enabled = false;
        }
        public void EscribirSuperU(string username)
        {
            labelSuperU.Visible = true;
            labelSuperU.Text = username + " conectado";
        }
        public void BorrarSuperU()
        {
            labelSuperU.Text ="";
        }
        public void BorrarChat()
        {
            listBoxChat.Items.Clear();
        }
        public void DesactRegistrar()
        {
            buttonSingUp.Enabled = false;
        }
        public void ActRegistrar()
        {
            buttonSingUp.Enabled = true;
        }
        public void ActDown()
        {
            buttonDown.Enabled = true;
        }
        public void DesactDown()
        {
            buttonDown.Enabled = false;
        }
        public void BorrarTBChat() 
        {
            textBoxChat.Text = "";
        }
        public void ActEnv()
        {
            buttonChat.Enabled = true;
        }
        public void DesactEnv()
        {
            buttonChat.Enabled = false;
        }
        public void BGGrey()
        {
            this.BackColor = Color.Gray; 
        }
        private void AtenderServidor()
        {
            int fin = 0;
            while (fin ==0)
            {
                int codigo;
                byte[] msg = new byte[80];
                server.Receive(msg);
                string mensaje = Encoding.ASCII.GetString(msg).Split('\0')[0];
                string[] trozos = mensaje.Split('/');
                codigo = Convert.ToInt32(trozos[0]);
                DelegadoParaBorrar delegadoBorrarRegistrar = new DelegadoParaBorrar(BorraTextBoxRegistrar);
                DelegadoParaBorrar delegadoBorrarIniciar = new DelegadoParaBorrar(BorraTextBoxIniciar);
                DelegadoParaEscribirList delegadoFilaList = new DelegadoParaEscribirList(AddRowList);
                DelegadoParaLimpiarList delegadoClearList = new DelegadoParaLimpiarList(ClearList);
                DelegadoParaDesactLogIn delegadoDesactLogIn = new DelegadoParaDesactLogIn(LoginDesact);
                DelegadoParaActLogIn delegadoActLogIn = new DelegadoParaActLogIn(LoginAct);
                DelegadoParaActDesconectar delegadoActDes = new DelegadoParaActDesconectar(DesconectAct);
                DelegadoParaDesactDesconectar delegadoDesactDes = new DelegadoParaDesactDesconectar(DesconectDesact);
                DelegadoParaEscribirInv delegadoEscInv = new DelegadoParaEscribirInv(EscribirInv);
                DelegadoParaBorrarInv delegadoBorrarInv = new DelegadoParaBorrarInv(BorrarInv);
                DelegadoParaActAccept delegadoActAccept = new DelegadoParaActAccept(ActAccept);
                DelegadoParaActDeny delegadoActDeny = new DelegadoParaActDeny(ActDeny);
                DelegadoParaActInv delegadoActInv = new DelegadoParaActInv(ActInv);
                DelegadoParaDesactInv delegadoDesactInv = new DelegadoParaDesactInv(DesactInv);
                DelegadoParaListBox delegadoListBox = new DelegadoParaListBox(AddItem);
                DelegadoParaEscribirSuperU delegadoEscribirSuperU = new DelegadoParaEscribirSuperU(EscribirSuperU);
                DelegadoParaBorrarSuperU delegadoBorrarSuperU = new DelegadoParaBorrarSuperU(BorrarSuperU);
                DelegadoParaDesactAccept delegadoDesactAccept = new DelegadoParaDesactAccept(DesactAccept);
                DelegadoParaDesactDeny delegadoDesactDeny = new DelegadoParaDesactDeny(DesactDeny);
                DelegadoParaBorrarChat delegadoBorrarChat = new DelegadoParaBorrarChat(BorrarChat);
                DelegadoParaDesactRegistrar delegadoDesactRegistrar = new DelegadoParaDesactRegistrar(DesactRegistrar);
                DelegadoParaActRegistrar delegadoActRegistrar = new DelegadoParaActRegistrar(ActRegistrar);
                DelegadoParaActDown delegadoActDown = new DelegadoParaActDown(ActDown);
                DelegadoParaDesactDown delegadoDesactDown = new DelegadoParaDesactDown(DesactDown);
                DelegadoParaActEnv delegadoActEnv = new DelegadoParaActEnv(ActEnv);
                DelegadoParaDesactEnv delegadoDesactEnv = new DelegadoParaDesactEnv(DesactEnv);
                DelegadoParaLimpiarList delegadoLimpiarList = new DelegadoParaLimpiarList(ClearList);
                DelegadoParaBGGray delegadoBG = new DelegadoParaBGGray(BGGrey);

                switch (codigo)
                {
                    case 0:
                        if (trozos[1] == "Si")
                        {
                            Invoke(delegadoBG, new object[] { });
                            Invoke(delegadoLimpiarList, new object[] { });                            
                            Invoke(delegadoActRegistrar, new object[] { });
                            Invoke(delegadoActLogIn, new object[] { });
                            Invoke(delegadoDesactDes, new object[] { });
                            Invoke(delegadoDesactInv, new object[] { });
                            Invoke(delegadoDesactAccept, new object[] { });
                            Invoke(delegadoDesactDeny, new object[] { });
                            Invoke(delegadoBorrarSuperU, new object[] {});
                            Invoke(delegadoBorrarChat, new object[] { });
                            Invoke(delegadoActDown, new object[] { });
                            Invoke(delegadoDesactEnv, new object[] { });
                            Invoke(delegadoDesactDes, new object[] { });
                            Invoke(delegadoBorrarInv, new object[] { });
                            MessageBox.Show("El usuario " + usuario + " ha sido desconectado correctamente");
                            fin = 1;
                        }
                        else if (trozos[1] == "No")
                        {
                            MessageBox.Show("Ha ocurrido un error al desconectar al usuario");
                        }
                        break;
                    case 1:
                        if (trozos[1] == "Si")
                        {
                            usuario = textBoxUsername.Text;
                            MessageBox.Show("Bienvenido " + textBoxUsername.Text + ", has iniciado sesión correctamente");
                            Invoke(delegadoBorrarIniciar, new object[] { });
                            Invoke(delegadoDesactLogIn, new object[] { });
                            Invoke(delegadoActDes, new object[] { });
                            Invoke(delegadoActInv, new object[] { });
                            Invoke(delegadoEscribirSuperU, new object[] {usuario});
                            Invoke(delegadoDesactDown, new object[] { });
                            Invoke(delegadoActEnv, new object[] { });
                            Invoke(delegadoDesactRegistrar, new object[] { });
                        }
                        else if (trozos[1] == "No")
                        {
                            MessageBox.Show("El nombre de usuario o contraseña son incorrectos o no existen");
                            fin = 1;
                        }
                        break;
                    case 2:
                        if (trozos[1] == "Si")
                        {
                            MessageBox.Show("Bienvenido/a " + textBoxUsername.Text + ", te has registrado correctamente");
                            Invoke(delegadoBorrarRegistrar, new object[] { });
                            fin = 1;
                        }
                        else if (trozos[1] == "No")
                        {
                            MessageBox.Show("Ha ocurrido un error al registrarse");
                            fin = 1;
                        }
                        else 
                        {
                            MessageBox.Show("El username que has indicado no está disponible");
                            fin = 1;
                        }
                        break;
                    case 3:
                        Invoke(delegadoClearList, new object[] { });
                        int i;
                        for (i = 1; i < trozos.Length-1; i++)
                            Invoke(delegadoFilaList, new object[] { trozos[i]});
                        break;
                    case 4:
                        if (trozos[1] == "Si")
                        {
                            Invoke(delegadoEscInv, new object[] { trozos[2] });
                            Invoke(delegadoActAccept, new object[] {});
                            Invoke(delegadoActDeny, new object[] { });
                            invitado = trozos[2];
                        }
                        else if (trozos[1] == "No")
                        {
                            MessageBox.Show("No existe ningún usuario conectado con ese username");
                        }

                        break;
                    case 5:                   
                            Invoke(delegadoListBox, new object[] { trozos[1] + ": " + trozos[2] });                      
                        break;
                    case 6:
                        if (trozos[1] == "Si")
                        {
                            MessageBox.Show("El usuario " + textBoxUserDown.Text + ", ha sido eliminado correctamente");
                            atender.Abort();
                        }
                        else if (trozos[1] == "No")
                        {
                            MessageBox.Show("El username no existe o la contraseña es incorrecta");
                            atender.Abort();
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error al dar de baja el usuario");
                            atender.Abort();
                        }
                        break;
                    case 7:
                        if (trozos[1] == "Si")
                        {
                            Form2 frm = new Form2();
                            frm.Show();

                        }
                        else if (trozos[1] == "Rechazado") MessageBox.Show("El usuario " + invitado + " ha rechazado tu invitación");

                        else Invoke(delegadoBorrarInv, new object[] { });

                        break;






                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, puerto);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            if (textBoxUsername.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Por favor introduzca un nombre de usuario y contraseña");
            }
            else
            {
                string mensaje = "1/"+ textBoxUsername.Text + "/"+ textBoxPassword.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);                              
                server.Send(msg);
                //byte[] msg2 = new byte[200];
                //server.Receive(msg2);
                //string answer = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                ThreadStart ts = delegate{ AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
        }

        private void buttonSingUp_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string user = textBoxUserLog.Text;
            string pass1 = textBoxPasswordLog1.Text;
            string pass2 = textBoxPasswordLog2.Text;
            string mensaje;
            if (user == "" || pass1 == "" || pass2 == "")
            {
                MessageBox.Show("Por favor, rellene todos los campos");
            }
            else if (pass1 != pass2) MessageBox.Show("Las contraseñas no coinciden");
            else
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("192.168.56.102");
                IPEndPoint ipep = new IPEndPoint(direc, puerto);


                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket
                    this.BackColor = Color.Green;
                    MessageBox.Show("Conectado");

                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }

                mensaje = "2/" + name + "/" + user + "/" + pass1;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            
        }

        private void buttonDesconectar_Click(object sender, EventArgs e)
        {
            string mensaje;
            mensaje = "0/"+usuario;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
        }

        private void buttonInv_Click(object sender, EventArgs e)
        {
            if (textBoxInv.Text == "")
                MessageBox.Show("Por favor introduzca el username del usuario al que desea invitar");
            else {
                
                string mensaje;
                mensaje = "4/" + textBoxInv.Text + "/" + usuario;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            string mensaje;
            mensaje = "7/Si/" + invitado + "/" + usuario;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            
        }

        private void buttonDeny_Click(object sender, EventArgs e)
        {
            string mensaje;
            mensaje = "7/No";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void buttonChat_Click(object sender, EventArgs e)
        {
            if (textBoxChat.Text == "")
                MessageBox.Show("No es posible envíar mensajes vacíos por el chat");
            else
            {
                string mensaje;
                mensaje = "5/" + usuario + "/" + textBoxChat.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                DelegadoParaBorrarTBChat delegadoBorrarTBChat = new DelegadoParaBorrarTBChat(BorrarTBChat);
                Invoke(delegadoBorrarTBChat, new object[] { });
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            string user = textBoxUserDown.Text;
            string pass1 = textBoxPassDown.Text;
            string pass2 = textBoxPassDown2.Text;
            string mensaje;
            if (user == "" || pass1 == "" || pass2 == "")
            {
                MessageBox.Show("Por favor, rellene todos los campos");
            }
            else if (pass1 != pass2) MessageBox.Show("Las contraseñas no coinciden");
            else
            {
                //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
                //al que deseamos conectarnos
                IPAddress direc = IPAddress.Parse("192.168.56.102");
                IPEndPoint ipep = new IPEndPoint(direc, puerto);


                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep);//Intentamos conectar el socket

                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }

                mensaje = "6/" + user + "/" + pass1;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }
    }
}
