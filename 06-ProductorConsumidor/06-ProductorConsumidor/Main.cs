using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace _06_ProductorConsumidor
{
    public partial class Main : Form
    {
        List<Label> labels;
        Thread hilo;
        Random r = new Random(DateTime.Now.Millisecond);
        int punteroProducir = 0;
        int punteroConsumir = 0;

        public Main()
        {
            InitializeComponent();
            this.ControlBox = false;
            inicio();
            CheckForIllegalCrossThreadCalls = false;
            hilo = new Thread(new ThreadStart(procesos));
            hilo.Start();

        }
        public void inicio()
        {
            labels = new List<Label>();
            labels.Add(l1);
            labels.Add(l2);
            labels.Add(l3);
            labels.Add(l4);
            labels.Add(l5);
            labels.Add(l6);
            labels.Add(l7);
            labels.Add(l8);
            labels.Add(l9);
            labels.Add(l10);
            labels.Add(l11);
            labels.Add(l12);
            labels.Add(l13);
            labels.Add(l14);
            labels.Add(l15);
            labels.Add(l16);
            labels.Add(l17);
            labels.Add(l18);
            labels.Add(l19);
            labels.Add(l20);
            labels.Add(l21);
            labels.Add(l22);
            labels.Add(l23);
            labels.Add(l24);
            labels.Add(l25);
            labels.Add(l26);
            labels.Add(l27);
            labels.Add(l28);
            labels.Add(l29);
            labels.Add(l30);
            labels.Add(l31);
            labels.Add(l32);
            labels.Add(l33);
            labels.Add(l34);
            labels.Add(l35);
            foreach (Label l in labels)
            {
                l.Text = " ";
            }
            labelProductor.Text = "Dormido";
            labelConsumidor.Text = "Dormido";
        }
        public void procesos()
        {
            while (true)
            {
                int rP = r.Next(0, 2);
                if (rP == 0)
                {
                    labelProductor.Text = "Intentando Ingresar";
                    producir();
                    labelProductor.Text = "Dormido";
                }
                else
                {
                    labelConsumidor.Text = "Intentando Ingresar";
                    consumir();
                    labelConsumidor.Text = "Dormido";
                }
            }
        }
        public void consumir()
        {
            int rP = r.Next(4, 9);
            int anterior = punteroConsumir;
            for (int i = 0; i < rP; i++)
            {

                if (punteroConsumir == 35)
                {
                    punteroConsumir = 0;
                }
                if (anterior == 35)
                {
                    anterior = 0;
                }
                if (labels.ElementAt<Label>(punteroConsumir).Text == " ")
                {
                    labels.ElementAt<Label>(anterior).Text = " ";
                    return;
                }
                labels.ElementAt<Label>(anterior).Text = " ";

                labels.ElementAt<Label>(punteroConsumir).Text = "C☼";
                Thread.Sleep(800);
                labelConsumidor.Text = "Trabajando";
                anterior = punteroConsumir;
                punteroConsumir++;
            }
            labels.ElementAt<Label>(anterior).Text = " ";
        }
        public void producir()
        {
            int rP = r.Next(4, 9);
            int anterior=punteroProducir;
            for (int i = 0; i < rP; i++)
            {
                
                if (punteroProducir == 35)
                {
                    punteroProducir = 0;
                }
                if (anterior == 35)
                {
                    anterior = 0;
                }
                if (labels.ElementAt<Label>(punteroProducir).Text == "☼")
                {
                    labels.ElementAt<Label>(anterior).Text = "☼";
                    return;
                }
                labels.ElementAt<Label>(anterior).Text = "☼";
                
                labels.ElementAt<Label>(punteroProducir).Text="P☼";
                Thread.Sleep(800);
                labelProductor.Text = "Trabajando";
                anterior = punteroProducir;
                punteroProducir++;
            }
            labels.ElementAt<Label>(anterior).Text = "☼";
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c;
            c = e.KeyChar;
            Console.WriteLine(c);
            if (c== (char)27)
            {
                hilo.Abort();
                this.Dispose();

            }
        }
    }
}
