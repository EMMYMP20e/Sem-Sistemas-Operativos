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

namespace SimuladorProcesoPorLotes
{
    public partial class main : Form
    {
        List<Proceso> lista;
        int cantidad;
        Thread hilo;
        bool interrupt=false, error=false, pause=false;
        public main()
        {
            inicio();
            procesos();
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            hilo = new Thread(new ThreadStart(procesado));
            simulacion();
        }
        private void inicio()
        {
            Form1 form = new Form1();
            form.ShowDialog();
            cantidad = form.getCantidad();
        }
        private void procesos()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            string[] names = { "Miguel", "Edgar", "Marco", "Carlos", "Ulises", "Julio", "Hugo", "Saul", "Luis", "Juan" };
            string[] opes = { "+", "-", "/", "%", "*" };

            lista= new List<Proceso>(); ;
            for (int i = 0; i < cantidad; i++)
            {
                int rName= r.Next(0, 9);
                int rUno = r.Next(0, 9);
                int rDos = r.Next(1, 9);
                int rOpe = r.Next(0, 4);
                int rTime = r.Next(7, 18);
                string ecuacion = rUno.ToString() + opes[rOpe] + rDos.ToString();
                Proceso proc = new Proceso(names[rName],(i+1).ToString(),ecuacion,rTime);
                lista.Add(proc);
                
            }
        }
        public void simulacion()
        {            
            hilo.Start();
        }
        
        public void procesado()
        {
            int cont = 0;
            int sig = 0;
            int rest = cantidad/3;
            int total, restante;
            int lote = 1;
            int conto = 0;
            labelContador.Text = sig.ToString();
            labelPendientes.Text = rest.ToString();
            List<Proceso> aux = new List<Proceso>();
            List<Proceso> aux2 = new List<Proceso>();
            foreach (Proceso p in lista)
            {
                listBox1.Items.Add(p.getID() +"\t\t"+ p.getTime()+"\t"+p.getTrans());
                aux.Add(p);
                cont++; 
                if ((cont % 3) == 0 || cont == cantidad) { 
                    while(aux.Count>0)
                    {
                        Proceso n = aux.First<Proceso>();
                        labelNombre.Text = n.getName();
                        labelID.Text = n.getID();
                        labelOpe.Text = n.getOpe();
                        labelTme.Text = n.getTime().ToString();
                        total = 0;
                        restante = n.getTime();
                        labelTt.Text = n.getTrans().ToString();
                        labelTr.Text = (n.getTime()-n.getTrans()).ToString();
                        int i = n.getTrans();
                        if (n.getTrans() != 0)
                        {
                            total = i;
                            restante = n.getTime() - i;
                        }
                        for (int j=i; j < n.getTime(); j++)
                        {
                            
                            this.Refresh();
                            if (!pause)
                            {
                                Thread.Sleep(700);
                            }
                            else
                            {
                                try
                                {
                                    Thread.Sleep(Timeout.Infinite);
                                }
                                catch (ThreadInterruptedException)
                                {}
                            }
                            total++;
                            conto++;
                            restante--;
                            labelTt.Text = total.ToString();
                            labelTr.Text = restante.ToString();
                            labelContador.Text = conto.ToString();
                            if (error)
                            {
                                j = n.getTime();
                            }
                            if (interrupt)
                            {
                                n.setTrans(total);
                                j = n.getTime();
                                listBox1.Items.Add(n.getID() + "\t\t" + n.getTime() + "\t" + n.getTrans());
                                aux.Add(n);
                            }
                        }
                        listBox1.Items.RemoveAt(0);
                        if (interrupt)
                        {
                            interrupt = false;
                        }
                        else
                        {
                            if (error)
                            {
                                listBox2.Items.Add(n.getID() + " \t" + n.getOpe() + "\t\tError");
                                error = false;
                            }
                            else
                            {
                                n.resolver();
                                listBox2.Items.Add(n.getID() + " \t" + n.getOpe() + "\t\t" + n.getResult().ToString());
                            }
                        }
                        sig++;
                        aux.RemoveAt(0);
                    }
                    lote++;
                    rest--;
                    if (rest == -1)
                    {
                        rest = 0;
                    }
                    listBox2.Items.Add("-----------------------------------------------------");
                    labelPendientes.Text = rest.ToString();
                    aux.Clear();
                }
            }
        MessageBox.Show("Fin de Simulación");
        }

        private void main_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c;
            c = e.KeyChar;
            switch (c)
            {
                case 'e':
                    error = true;
                    break;
                case 'i':
                    interrupt = true;
                    break;
                case 'p':
                    pause = true;
                    break;
                case 'c':
                    if (pause)
                    {
                        hilo.Interrupt();
                    }
                    pause = false;
                    error = false;
                    interrupt = false;
                    break;
            }
        }
    }
}
