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
            int rest = cantidad/3;
            int total, restante;
            int lote = 1;
            int conto = 0;
            labelContador.Text = sig.ToString();
            /*if (cantidad % 3 != 0)
            {
                rest++;
            }*/
            labelPendientes.Text = rest.ToString();
            List<Proceso> aux = new List<Proceso>();
            foreach (Proceso p in lista)
            {
                listBox1.Items.Add(p.getName() +"\t\t"+ p.getTime());
                aux.Add(p);
                cont++; 
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
                            Thread.Sleep(1200);
                            total++;
                            conto++;
                            restante--;
                            labelTt.Text = total.ToString();
                            labelTr.Text = restante.ToString();
                            labelContador.Text = conto.ToString();
                        }
                        listBox1.Items.RemoveAt(0);
                        n.resolver();
                        listBox2.Items.Add(n.getID() + " \t" + n.getOpe() + "=" + n.getResult().ToString() + "\t\t "+ lote.ToString());
                        sig++;
                    }
                    lote++;
                    rest--;
                    if (rest == -1)
                    {
                        rest = 0;
                    }
                    listBox2.Items.Add("-------------------------------------------------");
                    labelPendientes.Text = rest.ToString();
                    aux.Clear();
                }
            }
            MessageBox.Show("Fin de Simulación");
        }
    }
}
