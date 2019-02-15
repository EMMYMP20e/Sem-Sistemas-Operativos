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
        List<Proceso> list;
        public Form2(List<Proceso> nueva)
        {

            InitializeComponent();
            this.ControlBox = false;
            list = nueva;
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            Proceso actual = new Proceso(textNombre.Text, textID.Text, textOpe.Text, (int)textTime.Value);
            foreach (Proceso a in list)
            {
                if (a.getID() == actual.getID())
                {
                    MessageBox.Show("ID no válido");
                    return;
                }
            }
            if (!actual.esValido())
            {
                MessageBox.Show("Operación no válida");
            }
            else if (textNombre.Text == "")
            {
                MessageBox.Show("Error: Nombre vacío");
            }
            else if (textID.Text == "")
            {
                MessageBox.Show("Error: ID vacío");
            }
            else
            {
                list.Add(actual);
                this.Hide();
            }

        }
    }
}
