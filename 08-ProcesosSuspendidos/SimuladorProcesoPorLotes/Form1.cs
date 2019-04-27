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
        int quantum;
        public Form1()
        {
            InitializeComponent();
            this.ControlBox = false;
        }
        public int getCantidad() { return cantidad; }

        public int getQuantum() { return quantum; }

        private void IngresaButton_Click(object sender, EventArgs e)
        {
            cantidad = (int)cantidadBox.Value;
            quantum = (int)quantumBox.Value;
            this.Hide();
        }
    }
}
