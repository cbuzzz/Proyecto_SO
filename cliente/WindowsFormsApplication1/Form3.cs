using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        
        int code;
        string contrincante;
        delegate void DelegadoParaClearDGV();
        delegate void DelegadoParaFila(string fila);
        int nForm;
        Socket server;
        string[] jugadores;

        public Form3(int nForm, Socket server)
        {

            InitializeComponent();
            this.server = server;
            this.nForm = nForm;
        }
        public void ClearList()
        {
            this.dataGridView1.Rows.Clear();
        }
        public void AñadirFila(string trozo)
        {
            int rowID = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[rowID];
            row.Cells[0].Value = trozo;

        }
        public void TomarRespuesta10(string[] jugadores)
        {
            DelegadoParaClearDGV delegadoClearList = new DelegadoParaClearDGV (ClearList);
            DelegadoParaFila delegadoFila = new DelegadoParaFila(AñadirFila);
            this.jugadores = jugadores;
            Invoke(delegadoClearList, new object[] { });
            int i;
            for (i = 1; i < jugadores.Length - 1; i++)
                Invoke(delegadoFila, new object[] { jugadores[i] });

        }



        private void Form3_Load(object sender, EventArgs e)
        {

        }

        //Escribe todos los jugadores registrados en la base de datos
        public void PonTodosLosJugadoresEnMatriz(string mensaje)
        {
            //dataGridView1.ColumnCount = 1;
            //dataGridView1.ColumnHeadersVisible = true;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.Columns[0].HeaderText = "Esta es la lista de jugadores registrados: ";
            //string[] piezas = mensaje.Split('/');
            //code = Convert.ToInt32(piezas[0]);
            //dataGridView1.RowCount = code;
            //int i = 0;
            //while (i < code)
            //{
            //    string[] nombres = new string[10];
            //    nombres[i] = piezas[i + 1];
            //    dataGridView1.Rows[i].Cells[0].Value = nombres[i];

            //    i++;
            //}

        }

        //Escribe resultado de la partida jugada con algun jugador 
        public void PonResultadosPartida(string mensaje)
        {
            //string[] piezas = mensaje.Split('/');
            //code = Convert.ToInt32(piezas[0]);
            //if (code == 0)
            //    MessageBox.Show("NO has jugado ninguna partida con el jugador seleccionado");
            //else
            //{
            //    dataGridView1.ColumnCount = 2;
            //    dataGridView1.ColumnHeadersVisible = true;
            //    dataGridView1.RowHeadersVisible = false;
            //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //    dataGridView1.RowCount = code;
            //    int i = 0;
            //    while (i < code)
            //    {
            //        string[] nombres = new string[10];
            //        nombres[i] = piezas[i + 1];
            //        dataGridView1.Rows[i].Cells[0].Value = "Ganador: ";
            //        dataGridView1.Rows[i].Cells[1].Value = nombres[i];

            //        i++;
            //    }
            //    dataGridView1.Refresh();
            //}
        }

        //Escribe los jugadores con los que has jugado recientemente
        public void PonJugadoresEnMatriz(string mensaje)
        {
            //string[] piezas = mensaje.Split('/');
            //code = Convert.ToInt32(piezas[0]);
            //if (code == 0)
            //    MessageBox.Show("NO has jugado ninguna partida con NINGÚN jugador");
            //else
            //{
            //    dataGridView1.ColumnCount = 1;
            //    dataGridView1.ColumnHeadersVisible = true;
            //    dataGridView1.RowHeadersVisible = false;
            //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //    dataGridView1.Columns[0].HeaderText = "Estos son los jugadores con los que has jugado una partida ";
            //    dataGridView1.RowCount = code;
            //    int i = 0;
            //    while (i < code)
            //    {
            //        string[] nombres = new string[10];
            //        nombres[i] = piezas[i + 1];
            //        dataGridView1.Rows[i].Cells[0].Value = nombres[i];

            //        i++;
            //    }
            //    dataGridView1.Refresh();
            //}
        }
        //
        //Pone en la matriz todos los jugadores registrados
        //
        public void PonInvokeTodosJugadores(string pieces)
        {
            //DelegadorParaEstadisticas delegadoEstad = new DelegadorParaEstadisticas(PonTodosLosJugadoresEnMatriz);
            //dataGridView1.Invoke(delegadoEstad, new object[] { pieces });
        }
        

        //
        //Pone en la matriz todos los jugadores con quien se ha jugado alguna aprtida
        //
        public void PonInvokeJugador(string pieces)
        {
            //DelegadorParaEstadisticas delegadoEstad = new DelegadorParaEstadisticas(PonJugadoresEnMatriz);
            //dataGridView1.Invoke(delegadoEstad, new object[] { pieces });
        }



        private void registrados_Click(object sender, EventArgs e)
        {
            
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mensaje = "10/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //contrincante = NombreRival.Text;
            //string mensaje = "11/" + contrincante;
            //byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            //server.Send(msg);
        }

        private void button4_Click(object sender, EventArgs e)
        {            
            this.Close();            
        }


    }
}
