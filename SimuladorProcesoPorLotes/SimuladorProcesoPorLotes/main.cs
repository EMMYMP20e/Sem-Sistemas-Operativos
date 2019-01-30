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
    public partial class main : Form
    {
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
            List<Proceso> lista = new List<Proceso>(); ;
            for (int i = 0; i < cantidad; i++)
            {
                Form2 form = new Form2(lista);
                form.ShowDialog();
                
            }
        }
    }
}
