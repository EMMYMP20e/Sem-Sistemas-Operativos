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
    public partial class Form2 : Form
    {
        public Form2(List<Proceso> list)
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            Proceso actual = new Proceso(textNombre.Text, textID.Text, textOpe.Text, (int)textTime.Value);
            foreach (Proceso a in lista)
            {
                if (a.getID() == actual.getID())
                {
                    MessageBox.Show("Proceso no válido");
                    return;
                }
            }
            if (!actual.esValido())
            {
                MessageBox.Show("Proceso no válido");
            }
            else
            {
                lista.Add(actual);
                this.Hide();
            }

        }
    }
}
