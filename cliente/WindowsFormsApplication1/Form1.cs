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

namespace WindowsFormsApplication1
{
    public partial class Cliente : Form
    {
        Socket server;
        Thread atender;
        delegate void DelegadoParaBorrar();
        delegate void DelegadoParaDGV(string conectados,int i);
        int puerto = 9060;
        public Cliente()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
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
        public void AddRow(string trozo,int i)
        {
            int rowID = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[rowID];
            row.Cells[0].Value = trozo;
            
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
                DelegadoParaDGV delegadoFila = new DelegadoParaDGV(AddRow);

                switch (codigo)
                {
                    case 1:
                        if (trozos[1] == "Si")
                        {
                            MessageBox.Show("Bienvenido " + textBoxUsername.Text + ", has iniciado sesión correctamente");
                            Invoke(delegadoBorrarIniciar, new object[] { });
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
                            MessageBox.Show("Bienvenido " + textBoxUsername.Text + ", te has registrado correctamente");
                            Invoke(delegadoBorrarRegistrar, new object[] {});
                        }
                        else if (trozos[1] == "No")
                        {
                            MessageBox.Show("Ha ocurrido un error al registrarse");
                            fin = 1;
                        }
                        break;
                    case 3:
                        
                        int i;
                        for (i = 0; i < trozos.Length-1; i++)
                            Invoke(delegadoFila, new object[] { trozos[i+1],i });
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
                byte[] SQLserver = System.Text.Encoding.ASCII.GetBytes(mensaje);                              
                server.Send(SQLserver);
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
            string answer;
            if (user == "" || pass1 == "" || pass2 == "")
            {
                MessageBox.Show("Por favor, rellene todos los campos");
            }
            else if (pass1 != pass2) MessageBox.Show("Las contraseñas no coinciden");

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
            //byte[] msg2 = new byte[200];
            //server.Receive(msg2);
            //answer = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

        }

    }
}
