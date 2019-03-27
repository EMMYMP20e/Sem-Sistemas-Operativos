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
            list = n;
            Despliega();
        }
        private void Despliega()
        {
            int retorno;
            string respuesta;
            //list.Sort((x, y) => x.getID().CompareTo(y.getID()));
            foreach (Proceso p in list)
            {
                listBox1.Items.Add("\tID: " + p.getID()+"\n");
                if (p.getError())
                {
                    respuesta = "Error";
                }
                else
                {
                    respuesta = p.getRespuesta().ToString();
                }
                listBox1.Items.Add(" Ope: " + p.getOpe()+ " = "+respuesta+"\n");
                listBox1.Items.Add(" TME: " + p.getTime() + "\n");
                listBox1.Items.Add(" T de Llegada: " + p.getLlegada() + "\n");
                listBox1.Items.Add(" T de Finalización: " + p.getFinalizacion() + "\n");
                retorno = p.getFinalizacion() - p.getLlegada();
                listBox1.Items.Add(" T de Retorno: " + retorno  + "\n");
                listBox1.Items.Add(" T de Servicio: " + p.getServicio() + "\n");
                listBox1.Items.Add(" T de Espera: " + (retorno-p.getServicio())+ "\n");
                listBox1.Items.Add(" T de Respuesta: " + p.getRespuesta() + "\n");
                listBox1.Items.Add("---------------------------------------------");
            }
        }
    }
}
