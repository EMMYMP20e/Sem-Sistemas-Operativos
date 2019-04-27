using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorProcesoPorLotes
{
    public partial class BCP : Form
    {
        List<Proceso> list;
        public BCP(List<Proceso> n)
        {
            InitializeComponent();
            Rectangle r = Screen.FromPoint(this.Location).WorkingArea;
            //int x = r.Left + (r.Width - this.Width) / 2;
            int x = r.Right-this.Width;
            int y = r.Top +(r.Height-this.Height)/2;
            this.Location = new Point(x, y);
            list = n;
            Despliega();
        }
        private void Despliega()
        {
            int retorno=0;
            string respuesta;
            foreach (Proceso p in list)
            {
                listBox1.Items.Add("\tID: " + p.getID() + "\n");
                /*if (p.getTime() == p.getServicio())
                {
                    p.setEstado(3);
                }*/
                switch (p.getEstado())
                {
                    case 0:
                        listBox1.Items.Add("Estado: En Sistema\n");
                        listBox1.Items.Add(" Ope: " + p.getOpe() + "\n");
                        listBox1.Items.Add(" T de Llegada: " + p.getLlegada() + "\n");
                        listBox1.Items.Add(" T de Espera: " + p.getEspera() + "\n");
                        listBox1.Items.Add(" T de Servicio: " + p.getServicio() + "\n");
                        listBox1.Items.Add(" T Restante en CPU: " + (p.getTime() - p.getServicio()) + "\n");
                        listBox1.Items.Add(" T de Respuesta: " + p.getRespuesta() + "\n");
                        listBox1.Items.Add(" TME: " + p.getTime() + "\n");
                        break;
                    case 1:
                        listBox1.Items.Add("Estado: Nuevo\n");
                        listBox1.Items.Add(" Ope: " + p.getOpe() + "\n");
                        break;
                    case 2:
                        listBox1.Items.Add("Estado: Bloqueado\n");
                        listBox1.Items.Add(" Ope: " + p.getOpe() + "\n");
                        listBox1.Items.Add(" T de Llegada: " + p.getLlegada() + "\n");
                        listBox1.Items.Add(" T de Espera: " + p.getEspera() + "\n");
                        listBox1.Items.Add(" T de Servicio: " + p.getServicio() + "\n");
                        listBox1.Items.Add(" T Restante en CPU: " + (p.getTime() - p.getServicio()) + "\n");
                        listBox1.Items.Add(" T Restante Bloqueado: " + (9 - p.getBloqueado()) + "\n");
                        listBox1.Items.Add(" T de Respuesta: " + p.getRespuesta() + "\n");
                        listBox1.Items.Add(" TME: " + p.getTime() + "\n");
                        break;
                    case 3:
                        if (p.getError())
                        {
                            respuesta = "Error";
                        }
                        else
                        {
                            respuesta = p.getResult().ToString();
                        }
                        listBox1.Items.Add("Estado: Terminado\n");
                        listBox1.Items.Add(" Ope: " + p.getOpe() + " = " + respuesta + "\n");
                        listBox1.Items.Add(" TME: " + p.getTime() + "\n");
                        listBox1.Items.Add(" T de Llegada: " + p.getLlegada() + "\n");
                        listBox1.Items.Add(" T de Finalización: " + p.getFinalizacion() + "\n");
                        retorno = p.getFinalizacion() - p.getLlegada();
                        listBox1.Items.Add(" T de Retorno: " + retorno + "\n");
                        listBox1.Items.Add(" T de Servicio: " + p.getServicio() + "\n");
                        listBox1.Items.Add(" T de Espera: " + (retorno-p.getServicio()) + "\n");
                        listBox1.Items.Add(" T de Respuesta: " + p.getRespuesta() + "\n");
                        break;
                }
                listBox1.Items.Add("---------------------------------------------");
            }
        }

    }
}
