﻿using System;
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

        int barcos;
        int round;
        int PuntosJugador;
        int PuntosEnemigo;
        int nForm;
        string j1;
        string j2;
        Socket server;
        string mensajeE;
        string[] barcosEnemigo = new string[17];
        

        public Form2(int nForm, Socket server, string j1, string j2)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.j1 = j1;
            this.j2 = j2;
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
            if (PosicionJugadorButton.Count > 0 && round > 0)
            {
                round -= 1;

                txtRondas.Text = "Round: " + round;

                int index = random.Next(PosicionJugadorButton.Count);

                if ((string)PosicionJugadorButton[index].Tag == "BarcoJugador")
                {
                    PosicionJugadorButton[index].BackgroundImage = imageList1.Images[0];
                    EnemigoUbi.Text = PosicionJugadorButton[index].Text;
                    PosicionJugadorButton[index].Enabled = false;
                    PosicionJugadorButton[index].BackColor = Color.DarkBlue;
                    PosicionJugadorButton.RemoveAt(index);
                    PuntosEnemigo += 1;
                    labelPJ2.Text = PuntosEnemigo.ToString();
                    TimerEnemigo.Stop();
                }
                else
                {
                    PosicionJugadorButton[index].BackgroundImage = imageList1.Images[1];
                    EnemigoUbi.Text = PosicionJugadorButton[index].Text;
                    PosicionJugadorButton[index].Enabled = false;
                    PosicionJugadorButton[index].BackColor = Color.DarkBlue;
                    PosicionJugadorButton.RemoveAt(index);
                    TimerEnemigo.Stop();
                }
            }

            if (round < 1)
            {

                if (PuntosJugador > PuntosEnemigo)
                {
                    MessageBox.Show("Ganaste!");
                    round = 50;
                    ReiniciarJuego();
                }
                else if (PuntosEnemigo > PuntosJugador)
                {
                    MessageBox.Show("Perdiste bobo");
                    round = 50;
                    ReiniciarJuego();
                }
                else if (PuntosEnemigo == PuntosJugador)
                {
                    MessageBox.Show("Empataste");
                    round = 50;
                    ReiniciarJuego();
                }
            }
        }

        private void AtacarButtonEvent(object sender, EventArgs e)
        {
            if (EnemigoUbiTextbox.Text != "")
            {

                var attackPosition = EnemigoUbiTextbox.Text;

                var attackPositionTranslate = Traducir(EnemigoUbiTextbox.Text);

                int index = PosicionEnemigoButton.FindIndex(a => a.Name == attackPositionTranslate);
                //int i = Convert.ToInt32(attackPositionTranslate);
                //string mensaje = "8/" + i;
                //byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                //server.Send(msg);

                if (PosicionEnemigoButton[index].Enabled && round > 0)
                {
                    round -= 1;
                    txtRondas.Text = "Round: " + round;


                    if ((string)PosicionEnemigoButton[index].Tag == "BarcoEnemigo")
                    {

                        PosicionEnemigoButton[index].Enabled = false;
                        PosicionEnemigoButton[index].BackgroundImage = imageList1.Images[0]; //llamo al icono de fuego
                        PosicionEnemigoButton[index].BackColor = Color.DarkBlue;
                        PuntosJugador += 1;//en este caso acierta, por lo tanto le suma un punto
                        labelPJ1.Text = PuntosJugador.ToString();
                        TimerEnemigo.Start();

                    }
                    else
                    {
                        PosicionEnemigoButton[index].Enabled = false;
                        PosicionEnemigoButton[index].BackgroundImage = imageList1.Images[1]; //llamo al icono de fallo
                        PosicionEnemigoButton[index].BackColor = Color.DarkBlue;
                        TimerEnemigo.Start();
                    }
                }


            }
            else
            {
                MessageBox.Show("Elige una ubicación", "Informacion");
            }
        }

        private void J1posicionEvent(object sender, EventArgs e)
        {
            if (barcos == 17) 
            {
                var button = (Button)sender;
                mensajeE = "8/"+nForm+"/"+j2+"/"+button.Text+"/";
                button.Enabled = false;
                button.Tag = "BarcoJugador";
                button.BackColor = Color.Orange;
                barcos -= 1;
            }
            else if (barcos > 0)
            {
                var button = (Button)sender;
                mensajeE = mensajeE +  button.Text+"/";
                button.Enabled = false;
                button.Tag = "BarcoJugador";
                button.BackColor = Color.Orange;
                barcos -= 1;
            }
            else if (barcos == 0)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensajeE);
                server.Send(msg);

                btnAtacar.Enabled = true;
                btnAtacar.BackColor = Color.Red;
                btnAtacar.ForeColor = Color.White;
                //una vez estén todos los barcos propios marcados, el botón de atacar se vuelve rojo (está a punto) ya para atacar
                txtHelp.Text = "2) Ahora escribe donde quieres atacar";
                PickearUbiEnemigoJ2();
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
        public string Traducirinverso(string S)
        {
            char[] chars2 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            char[] chars1 = { 'k', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T' };
            char[] respuesta = { 'A', 'B' };
            char[] respuesta2 = { 'A', 'B', 'C' };
            char letra = S[0];
            char num = S[1];
            if (S.Length == 3)
            {
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
                    if (letra == chars2[i])
                    {
                        respuesta[0] = chars1[i];
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

            EnemigoUbiTextbox.Clear();

            EnemigoUbiTextbox.Text = null;

            txtHelp.Text = "1) Clica en 9 casillas para poner tu flota.";


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
            round = 50;
            barcos = 17;

            labelPJ1.Text = PuntosJugador.ToString();
            labelPJ2.Text = PuntosEnemigo.ToString();
            EnemigoUbi.Text = "--";

            btnAtacar.Enabled = false;

 

        }

        private void PickearUbiEnemigo()
        {

            for (int i = 0; i < 100; i++)
            {
                int index = random.Next(PosicionEnemigoButton.Count);

                if (PosicionEnemigoButton[index].Enabled == true && (string)PosicionEnemigoButton[index].Tag == null)
                {
                    PosicionEnemigoButton[index].Tag = "BarcoEnemigo";

                }
                else
                {
                    index = random.Next(PosicionEnemigoButton.Count);
                }
            }


        }
        private void PickearUbiEnemigoJ2()
        {
            int j = 0;
            for (int i = 0; i < PosicionEnemigoButton.Count-1; i++)
            {

                
                if (PosicionEnemigoButton[i].Enabled == true && (string)PosicionEnemigoButton[i].Tag == null && Traducir(PosicionEnemigoButton[i].Text) == barcosEnemigo[j] )
                {

                    PosicionEnemigoButton[i].Tag = "BarcoEnemigo";
                    j++;
                }
                
            }


        }
        public void TomarRespuesta8(string[] barcos)
        {
            this.barcosEnemigo = barcos;
        }
        private void labelTJ1_Click(object sender, EventArgs e)
        {

        }
    }
}
