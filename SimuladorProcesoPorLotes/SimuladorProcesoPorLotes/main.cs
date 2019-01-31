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
        public main()
        {
            inicio();
            procesos();
            InitializeComponent();
        }
        private void inicio()
        {
            Form1 form = new Form1();
            form.ShowDialog();
            cantidad = form.getCantidad();
        }
        private void procesos()
        {
            lista= new List<Proceso>(); ;
            for (int i = 0; i < cantidad; i++)
            {
                Form2 form = new Form2(lista);
                form.ShowDialog();
            }
        }
        public void simulacion()
        {
            int cont = 0;
            int sig = 0;
            int espera=0;
            int rest = cantidad;
            int total, restante;
            labelContador.Text = sig.ToString();
            labelPendientes.Text = rest.ToString();
            List<Proceso> aux = new List<Proceso>();
            foreach (Proceso p in lista)
            {
                
                espera += p.getTime();
                listBox1.Items.Add(p.getName() +"\t\t"+ p.getTime());
                aux.Add(p);
                cont++;
                Console.WriteLine(cont % 3);
                if ((cont % 3) == 0|| cont == cantidad) { 
                    foreach (Proceso n in aux)
                    {
                        labelNombre.Text = n.getName();
                        labelID.Text = n.getID();
                        labelOpe.Text = n.getOpe();
                        labelTme.Text = n.getTime().ToString();
                        total = 0;
                        restante = n.getTime();
                        labelTt.Text = "0";
                        labelTr.Text = n.getTime().ToString();
                        
                        for (int i = 0; i < n.getTime(); i++)
                        {
                            this.Refresh();
                            Thread.Sleep(1300);
                            total++;
                            restante--;
                            labelTt.Text = total.ToString();
                            labelTr.Text = restante.ToString();
                        }
                        listBox1.Items.RemoveAt(0);
                        sig++;
                        rest--;
                        labelContador.Text = sig.ToString();
                        labelPendientes.Text = rest.ToString();
                    }
                    
                    aux.Clear();
                espera = 0;
                }
                
            }
            MessageBox.Show("Fin de Simulación");
        }
    }
}
