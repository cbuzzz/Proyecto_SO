using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {

        List<Button> PosicionJugadorButton;
        List<Button> PosicionEnemigoButton;
        
        Random random = new Random();

        delegate void DelegadoParaLblRondas(int rondas);
        delegate void DelegadoParaEnabled(int index);
        delegate void DelegadoParaBtnImageColor(int index, int h);
        delegate void DelegadoParaLblPoints(string puntos);
        delegate void DelegadoParaLblTurno(int turno);
        delegate void DelegadoParaLblPJ1(string puntos);
        delegate void DelegadoParaLblPJ2(string puntos);

        int barcos;
        int round;
        int PuntosJugador;
        int PuntosEnemigo;
        int nForm;
        string j1;
        string j2;
        int turno;
        Socket server;
        string[] mensajeE = new string[20];
        string[] barcosEnemigo = new string[17];
        string casillaAtacada;
        int barcosEnemigoRecibido = 0;
        int cont = 4;
        int index;
        int ubicados = 0;
        

        public void EscribirLblR(int rondas)
        {
            
            txtRondas.Text = "Round: "+rondas;   
        }
        public void EscribirLblPJ1(string puntos)
        {
            labelPJ1.Text = puntos;
        }
        public void EscribirLblPJ2(string puntos)
        {
            labelPJ2.Text = puntos;
        }
        public void EscribirLblP(string points)
        {   
            labelPJ1.Text = points;
        }
        public void EscribirLblT(int turno)
        {
            if (turno == 1) 
            {
                labelTurno.Text = "TU TURNO";
                labelTurno.BackColor = Color.Green;
                labelTurno.ForeColor = Color.White;
            }
            else
            {
                labelTurno.Text = "TURNO RIVAL";
                labelTurno.BackColor = Color.Red;
                labelTurno.ForeColor = Color.White;
            }
            
        }
        public void EnableBtnJ(int index)
        {

            if (PosicionJugadorButton[index].Enabled == true)
            {
                PosicionJugadorButton[index].Enabled = false;
            }
            
            if (PosicionJugadorButton[index].Enabled == false) 
            {
                PosicionJugadorButton[index].Enabled = true;
            }
            
        }
        public void EnableBtnE(int index)
        {

            
            if (PosicionEnemigoButton[index].Enabled == true)
            {
                PosicionEnemigoButton[index].Enabled = false;
            }
            
            if (PosicionEnemigoButton[index].Enabled == false)
            {
                PosicionEnemigoButton[index].Enabled = true;
            }
        }
        public void BtnImageEnemigo(int index, int h)
        {
            if (h == 1)
            {
                PosicionEnemigoButton[index].BackgroundImage = imageList1.Images[0]; //llamo al icono de fuego
                PosicionEnemigoButton[index].BackColor = Color.DarkBlue;
            }

            else if (h == 0)
            {
                
                PosicionEnemigoButton[index].BackgroundImage = imageList1.Images[1]; //llamo al icono de fallo
                PosicionEnemigoButton[index].BackColor = Color.DarkBlue;
            }


        }
        public void BtnImageJugador(int index, int h)
        {
            if (h == 1)
            {
                PosicionJugadorButton[index].BackgroundImage = imageList1.Images[0]; //llamo al icono de fuego
                PosicionJugadorButton[index].BackColor = Color.DarkBlue;
                
            }

            else if (h == 0) 
            {
                PosicionJugadorButton[index].BackgroundImage = imageList1.Images[1]; //llamo al icono de fallo
                PosicionJugadorButton[index].BackColor = Color.DarkBlue;
                
            }
            
        }
        

        public Form2(int nForm, Socket server, string j1, string j2, int turno)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.j1 = j1;
            this.j2 = j2;
            this.turno = turno;
            ReiniciarJuego();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.labelTJ1.Text = "Puntos " + j1 + ": ";
            this.labelTJ2.Text = "Puntos " + j2 + ": ";
        }

        private void button80_Click(object sender, EventArgs e)
        {

        }

        private void TimerEnemigoEvent(object sender, EventArgs e)
        {
            
        }


        public void CasillaAtacar(string casilla)
        {
            int h;
            DelegadoParaLblRondas delegadoLblR = new DelegadoParaLblRondas(EscribirLblR);
            DelegadoParaEnabled delegadoEnabled = new DelegadoParaEnabled(EnableBtnJ);
            DelegadoParaBtnImageColor delegadoBtnImageColorJ = new DelegadoParaBtnImageColor(BtnImageJugador);
            DelegadoParaLblPoints delegadoLblP = new DelegadoParaLblPoints(EscribirLblP);
            DelegadoParaLblPJ2 delegadoLblPJ2 = new DelegadoParaLblPJ2(EscribirLblPJ2);

            if (round > 0)
            {
                var attackPosition = casilla;

                index = PosicionJugadorButton.FindIndex(a => a.Name == attackPosition);
                round--;
                //txtRondas.Text = "Round: " + rondas;
                Invoke(delegadoLblR, new object[] { round });

                if ((string)PosicionJugadorButton[index].Tag == "BarcoJugador")
                {

                    h = 1;
                    
                    // PosicionJugadorButton[index].Enabled = false;
                    Invoke(delegadoEnabled, new object[] { index });
                    
                    // PosicionJugadorButton[index].BackgroundImage = imageList1.Images[0]; //llamo al icono de fuego
                    // PosicionJugadorButton[index].BackColor = Color.DarkBlue;
                    Invoke(delegadoBtnImageColorJ, new object[] { index, h });
                    PuntosEnemigo += 1;//en este caso acierta, por lo tanto le suma un punto
                     // labelPJ1.Text = PuntosJugador.ToString();
                    string points = PuntosEnemigo.ToString();
                    Invoke(delegadoLblPJ2, new object[] { points });
                    // TimerEnemigo.Start();
                    

                }
                else
                {
                    h = 0;
                    
                    // PosicionJugadorButton[index].Enabled = false;
                    Invoke(delegadoEnabled, new object[] { index });
                    
                    // PosicionJugadorButton[index].BackgroundImage = imageList1.Images[1]; //llamo al icono de fallo
                    // PosicionJugadorButton[index].BackColor = Color.DarkBlue;
                    Invoke(delegadoBtnImageColorJ, new object[] { index, h });
                    //TimerEnemigo.Start();
                    
                }
            }
            

            else
            {
                
                if (PuntosJugador > PuntosEnemigo)
                {
                    MessageBox.Show("¡Has ganado!");
                    round = 50;
                    ReiniciarJuego();
                }
                else if (PuntosEnemigo > PuntosJugador)
                {
                    MessageBox.Show("Has perdido");
                    round = 50;
                    ReiniciarJuego();
                }
                else if (PuntosEnemigo == PuntosJugador)
                {
                    MessageBox.Show("Habeis empatado");
                    round = 50;
                    ReiniciarJuego();
                }
                
            }
        }

        private void J1posicionEvent(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (barcos == 17)
            {
                mensajeE[0] = "8";
                mensajeE[1] = "" + nForm + "";
                mensajeE[2] = "" + j2 + "";
                mensajeE[3] = "" + button.Text + "";
                button.Tag = "BarcoJugador";
                button.BackColor = Color.Orange;
                barcos--;
            }
            else if (barcos > 1 && (button.Tag == null))
            {
                mensajeE[cont] = ""+button.Text+"";
                cont++;
                button.Tag = "BarcoJugador";
                button.BackColor = Color.Orange;
                barcos--;
            }
            else if (barcos > 1 && ((string)button.Tag == "BarcoJugador"))
            {
                for (int i = 0; i < mensajeE.Length; i++) {
                    if (mensajeE[i] == button.Text)
                    {
                        for(int j = i; j < mensajeE.Length-1; j++)
                        {
                            mensajeE[j] = mensajeE[j + 1];
                        }
                        mensajeE[mensajeE.Length-1] = "";
                        button.Tag = null;
                        button.BackColor = Color.White;
                    }
                }
                cont--;
                barcos++;
            }
            else if (barcos == 1)
            {
                mensajeE[cont] = "" + button.Text + "";
                cont++;
                button.Tag = "BarcoJugador";
                button.BackColor = Color.Orange;
                string mensaje = "";
                barcos--;
                for (int i = 0; i < mensajeE.Length; i++)
                {
                    mensaje = mensaje + mensajeE[i] + "/";
                }
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                buttonComenzar.Enabled = true;
                buttonComenzar.BackColor = Color.Red;
                buttonComenzar.ForeColor = Color.White;
            }
            
        }

        public string Traducir(string S)
        {
            char[] chars1 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            char[] chars2 = { 'k', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T' };
            char[] respuesta = { 'A', 'B' };
            char[] respuesta2 = { 'A', 'B','C' };
            char letra = S[0];
            char num = S[1];
            if (S.Length == 3) {
                char num2 = S[2];
                int encontrado = 0;
                int i = 0;
                while (encontrado == 0)
                {
                    if (letra == chars1[i])
                    {
                        respuesta2[0] = chars2[i];
                        respuesta2[1] = num;
                        respuesta2[2] = num2;
                        encontrado = 1;
                    }
                    i++;
                }
                string res = new string(respuesta2);
                return res;
            }
            else
            {
                int encontrado = 0;
                int i = 0;
                while (encontrado == 0)
                {
                    if (letra == chars1[i])
                    {
                        respuesta[0] = chars2[i];
                        respuesta[1] = num;
                        encontrado = 1;
                    }
                    i++;
                }
                string res = new string(respuesta);
                return res;
            }
            

        }
        

        private void ReiniciarJuego()
        {
            
            //asignamos a la lista todos los botones (coordenadas) del mapa a la posición de cada jugador
            PosicionEnemigoButton = new List<Button> { k1, k2, k3, k4, k5, k6, k7, k8, k9, k10, L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, M1, M2, M3, M4, M5, M6, M7, M8, M9, M10, N1,N2, N3, N4, N5, N6, N7, N8, N9, N10, O1, O2, O3, O4, O5, O6, O7, O8, O9, O10, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, R1, R2, R3, R4, R5, R6, R7, R8, R9, R10, S1, S2, S3, S4, S5, S6, S7, S8, S9, S10, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10};
            PosicionJugadorButton = new List<Button> { A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, E1, E2, E3, E4, E5, E6, E7, E8, E9, E10, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, G1, G2, G3, G4, G5, G6, G7, G8, G9, G10, H1, H2, H3, H4, H5, H6, H7, H8, H9, H10, I1, I2, I3, I4, I5, I6, I7, I8, I9, I10, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10};
            //DelegadoParaLblPJ1 delegadoLblPJ1 = new DelegadoParaLblPJ1(EscribirLblPJ1);
            //DelegadoParaLblPJ2 delegadoLblPJ2 = new DelegadoParaLblPJ2(EscribirLblPJ2);

            if (turno == 1)
            {
                labelTurno.Text = "TU TURNO";
                labelTurno.BackColor = Color.Green;
                labelTurno.ForeColor = Color.White;
            }
            else
            {
                labelTurno.Text = "TURNO RIVAL";
                labelTurno.BackColor = Color.Red;
                labelTurno.ForeColor = Color.White;
            }


            for (int i = 0; i < PosicionEnemigoButton.Count; i++)
            {
                PosicionEnemigoButton[i].Enabled = true;
                PosicionEnemigoButton[i].Tag = null;
                PosicionEnemigoButton[i].BackColor = Color.White;
                PosicionEnemigoButton[i].BackgroundImage = null;
                //EnemigoUbiTextbox = PosicionEnemigoButton[i].Text;
            }

            for (int i = 0; i < PosicionJugadorButton.Count; i++)
            {
                PosicionJugadorButton[i].Enabled = true;
                PosicionJugadorButton[i].Tag = null;
                PosicionJugadorButton[i].BackColor = Color.White;
                PosicionJugadorButton[i].BackgroundImage = null;
            }

            PuntosJugador = 0;
            PuntosEnemigo = 0;
            round = 10;
            barcos = 17;

            //Invoke(delegadoLblPJ1, new object[] { PuntosJugador.ToString() });
            //Invoke(delegadoLblPJ2, new object[] { PuntosEnemigo.ToString() });
            
        }

        
        private void PickearUbiEnemigoJ2()
        {
            for(int i = 0; i < PosicionEnemigoButton.Count - 1; i++)
            {
                if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[0])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[1])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[2])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[3])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[4])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[5])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[6])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[7])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[8])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[9])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[10])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[11])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[12])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[13])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[14])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[15])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
                else if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && PosicionEnemigoButton[i].Text == barcosEnemigo[16])
                {
                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                }
            }
        }
        public void TomarRespuesta8(string[] barcos)
        {
            this.barcosEnemigo = barcos;
            this.barcosEnemigoRecibido = 1;
        }
        public void TomarRespuesta9(string casilla)
        {
            this.casillaAtacada = casilla;
            CasillaAtacar(casillaAtacada);
            DelegadoParaLblTurno delegadoLblT = new DelegadoParaLblTurno(EscribirLblT);
            turno = 1;
            Invoke(delegadoLblT, new object[] { turno });
        }
        private void labelTJ1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAceptar_Click(object sender, EventArgs e) //boton aceptar es boton Comenzar, event creado antes de cambiar nombre
        {
            if (barcosEnemigoRecibido == 1)
            {
                PickearUbiEnemigoJ2();
                ubicados = 1;
                //una vez estén todos los barcos propios marcados, el botón de atacar se vuelve rojo (está a punto) ya para atacar
                buttonComenzar.Enabled= false;
                buttonComenzar.BackColor = Color.White;
                buttonComenzar.ForeColor = Color.Black;
                txtHelp.Text = "2) Ahora escribe donde quieres atacar";
            }
            else MessageBox.Show("Espera a que tu contrincante posicione sus barcos");
            
        }

        private void J2PosicionEvent(object sender, EventArgs e)
        {
            if (ubicados == 1 && turno == 1)
            {
                DelegadoParaLblTurno delegadoLblT = new DelegadoParaLblTurno(EscribirLblT);
                DelegadoParaLblPJ1 delegadoLblPJ1 = new DelegadoParaLblPJ1(EscribirLblPJ1);
                var button = (Button)sender;
                string mensaje = "9/" + nForm + "/" + button.Text + "/" + j2;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                DelegadoParaEnabled delegadoEnabled = new DelegadoParaEnabled(EnableBtnE);
                DelegadoParaBtnImageColor delegadoBtnImageColorE = new DelegadoParaBtnImageColor(BtnImageEnemigo);
                var attackPosition = Traducir(button.Text);
                int h;
                int indexE = PosicionEnemigoButton.FindIndex(a => a.Name == attackPosition);
                if ((string)PosicionEnemigoButton[indexE].Tag == "BarcoEnemigo")
                {

                    h = 1;

                    // PosicionJugadorButton[index].Enabled = false;
                    Invoke(delegadoEnabled, new object[] { indexE });

                    // PosicionJugadorButton[index].BackgroundImage = imageList1.Images[0]; //llamo al icono de fuego
                    // PosicionJugadorButton[index].BackColor = Color.DarkBlue;
                    Invoke(delegadoBtnImageColorE, new object[] { indexE, h });

                    PuntosJugador++;
                    Invoke(delegadoLblPJ1, new object[] { PuntosJugador.ToString() });


                }
                else
                {
                    h = 0;

                    // PosicionJugadorButton[index].Enabled = false;
                    Invoke(delegadoEnabled, new object[] { indexE });

                    // PosicionJugadorButton[index].BackgroundImage = imageList1.Images[1]; //llamo al icono de fallo
                    // PosicionJugadorButton[index].BackColor = Color.DarkBlue;
                    Invoke(delegadoBtnImageColorE, new object[] { indexE, h });
                    //TimerEnemigo.Start();

                }
                turno = 0;
                Invoke(delegadoLblT, new object[] { turno });

            }
            else if (ubicados == 1 && turno == 0) MessageBox.Show("¡Espera a que sea tu turno!");
            else if (ubicados == 0) MessageBox.Show("Antes de atacar debes seleccionar tus barcos");
        }
    }
}
