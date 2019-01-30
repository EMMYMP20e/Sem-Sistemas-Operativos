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
    public partial class Form1 : Form
    {
        int cantidad;
        public Form1()
        {
            InitializeComponent();
        }
        public int getCantidad() { return cantidad; }

        private void IngresaButton_Click(object sender, EventArgs e)
        {
            cantidad = (int)cantidadBox.Value;
            this.Hide();
        }
    }
}
