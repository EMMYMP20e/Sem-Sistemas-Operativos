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
        bool interrupt = false, error = false, pause = false, nuevo = false, mostrar=false;
        Random r = new Random(DateTime.Now.Millisecond);
        string[] opes = { "+", "-", "/", "%", "*" };
        int rest;

        List<Proceso> aux;
        List<Proceso> bloqueados;
        List<Proceso> terminados;
        List<Proceso> all;
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
            string[] opes = { "+", "-", "/", "%", "*" };

            lista = new List<Proceso>(); ;
            for (int i = 0; i < cantidad; i++)
            {
                agregaProceso(i);
            }
        }
        public void agregaProceso(int i)
        {
            int rName = r.Next(0, 9);
            int rUno = r.Next(0, 9);
            int rDos = r.Next(1, 9);
            int rOpe = r.Next(0, 4);
            int rTime = r.Next(7, 18);
            string ecuacion = rUno.ToString() + opes[rOpe] + rDos.ToString();
            Proceso proc = new Proceso((i + 1).ToString(), ecuacion, rTime);
            lista.Add(proc);
        }
        public void simulacion()
        {
            hilo.Start();
        }
        public void recolecta()
        {
            all = new List<Proceso>();
            bool first = true;
            all.Clear();
            foreach (Proceso p in aux)
            {
                p.setEstado(0);
                all.Add(p);
            }
            foreach (Proceso p in lista)
            {
                if (!first)
                {
                    p.setEstado(1);
                    all.Add(p);
                }
                first = false;
            }

            foreach (Proceso p in bloqueados)
            {
                p.setEstado(2);
                all.Add(p);
            }
            foreach (Proceso p in terminados)
            {
                p.setEstado(3);
                all.Add(p);
            }
        }
        public void procesado()
        {
            int cont = 0;
            int sig = 0;
            rest = cantidad;
            int total, restante;
            int conto = 0;
            int tme;
            int tres = 0;
            int servicioTranscurrido = 0;
            labelContador.Text = sig.ToString();
            labelPendientes.Text = cantidad.ToString();

            BCP bcp;
            aux = new List<Proceso>();
            bloqueados = new List<Proceso>();
            terminados = new List<Proceso>();
            
            Proceso p;
            Proceso nP;
            while (lista.Count != 0)
            {
                p = lista.First<Proceso>();
                listBox1.Items.Add(p.getID() + "\t\t" + p.getTime() + "\t" + p.getTrans());
                p.setLlegada(conto);
                aux.Add(p);
                cont++;
                tres++;
                rest--;
                labelPendientes.Text = rest.ToString();
                

                while (tres == 3 || cont == cantidad)
                {
                    while (aux.Count == 0)
                    {
                        labelID.Text = "";
                        labelOpe.Text = "";
                        labelTme.Text = "";
                        labelTt.Text = "";
                        labelTr.Text = "";
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
                            { }
                            /*if (mostrar)
                            {
                                mostrar = false;
                                recolecta();
                                bcp = new BCP(all);
                                bcp.ShowDialog();
                            }*/
                        }
                        if (bloqueados.Count > 0)
                        {
                            listBox3.Items.Clear();
                            int cDelete = 0;
                            foreach (Proceso b in bloqueados)
                            {
                                b.setBloqueado(b.getBloqueado() + 1);
                                b.setEspera(b.getEspera() + 1);
                                if (b.getBloqueado() < 10)
                                {
                                    listBox3.Items.Add(b.getID() + "\t\t" + b.getBloqueado());
                                }
                                else
                                {
                                    listBox1.Items.Add(b.getID() + "\t\t" + b.getTime() + "\t" + b.getTrans());
                                    b.setBloqueado(-1);
                                    aux.Add(b);
                                    cDelete++;
                                }
                            }
                            for (int k = 0; k < cDelete; k++)
                            {
                                bloqueados.RemoveAt(k);
                            }
                        }
                        conto++;
                        labelContador.Text = conto.ToString();
                        if (nuevo)
                        {
                            if (tres == 3)
                            {
                                //rest++;
                                labelPendientes.Text = rest.ToString();
                            }
                            else
                            {
                                nP = lista.Last<Proceso>();
                                listBox1.Items.Add(nP.getID() + "\t\t" + nP.getTime() + "\t" + nP.getTrans());
                                nP.setLlegada(conto);
                                aux.Add(nP);
                                tres++;
                                cont++;
                                rest--;
                                lista.RemoveAt(lista.Count - 1);
                            }
                            nuevo = false;
                        }

                    }
                    listBox1.Items.RemoveAt(0);
                    Proceso n = aux.First<Proceso>();
                    if (!n.getPrimera())
                    {
                        n.setRespuesta(conto - n.getLlegada());
                        n.setPrimera();
                    }
                    
                    labelID.Text = n.getID();
                    labelOpe.Text = n.getOpe();
                    labelTme.Text = n.getTime().ToString();
                    total = 0;
                    restante = n.getTime();
                    labelTt.Text = n.getTrans().ToString();
                    labelTr.Text = (n.getTime() - n.getTrans()).ToString();
                    int i = n.getTrans();
                    if (n.getTrans() != 0)
                    {
                        total = i;
                        restante = n.getTime() - i;
                    }
                    tme = n.getTime();
                    if (aux.Count == 0)
                    {
                        tme = 10;
                    }
                    for (int j = i; j < tme; j++)
                    {
                        n.setServicio(total+1);
                        foreach (Proceso d in aux)
                        {
                            if (d.getID() != n.getID())
                            {
                                d.setEspera(d.getEspera() + 1);
                            }

                        }
                        if (!pause)
                        {
                            Thread.Sleep(800);
                        }
                        else
                        {

                            try
                            {
                                Thread.Sleep(Timeout.Infinite);
                            }
                            catch (ThreadInterruptedException)
                            { }
                            /*if (mostrar)
                            {
                                mostrar = false;
                                recolecta();
                                bcp = new BCP(all);
                                bcp.ShowDialog();
                            }*/
                        }
                        //this.Refresh();

                        total++;
                        restante--;
                        
                        labelTt.Text = total.ToString();
                        labelTr.Text = restante.ToString();
                        conto++;
                        labelContador.Text = conto.ToString();
                        
                        
                        
                        if (error)
                        {
                            servicioTranscurrido = j;
                            j = n.getTime();
                        }
                        if (interrupt)
                        {
                            n.setTrans(total);
                            j = n.getTime();
                            bloqueados.Add(n);
                        }
                        if (nuevo)
                        {
                            if (tres == 3)
                            {
                                //rest++;
                                labelPendientes.Text = rest.ToString();
                            }
                            else
                            {
                                nP = lista.Last<Proceso>();
                                listBox1.Items.Add(nP.getID() + "\t\t" + nP.getTime() + "\t" + nP.getTrans());
                                nP.setLlegada(conto);
                                aux.Add(nP);
                                tres++;
                                cont++;
                                rest--;
                                lista.RemoveAt(lista.Count-1);
                            }
                            nuevo = false;
                        }
                        if (bloqueados.Count > 0)
                        {
                            listBox3.Items.Clear();
                            int cDelete = 0;
                            foreach (Proceso b in bloqueados)
                            {
                                b.setBloqueado(b.getBloqueado() + 1);
                                b.setEspera(b.getEspera() + 1);
                                if (b.getBloqueado() < 10)
                                {
                                    listBox3.Items.Add(b.getID() + "\t\t" + b.getBloqueado());
                                }
                                else
                                {
                                    listBox1.Items.Add(b.getID() + "\t\t" + b.getTime() + "\t" + b.getTrans());
                                    b.setBloqueado(-1);
                                    aux.Add(b);
                                    cDelete++;
                                }
                            }
                            for (int k = 0; k < cDelete; k++)
                            {
                                bloqueados.RemoveAt(k);

                            }
                        }
                        if (bloqueados.Count == 3 || (bloqueados.Count + listBox2.Items.Count) == cantidad)
                        {
                            labelID.Text = "";
                            labelOpe.Text = "";
                            labelTme.Text = "";
                            labelTt.Text = "";
                            labelTr.Text = "";
                        }
                    }

                    if (interrupt)
                    {
                        interrupt = false;
                    }
                    else
                    {
                        if (error)
                        {
                            if (bloqueados.Count != 3 && (bloqueados.Count + listBox2.Items.Count) != cantidad)
                            {
                                listBox2.Items.Add(n.getID() + " \t" + n.getOpe() + "\t\tError");
                                tres--;
                                error = false;
                                n.setFinalizacion(conto);
                                n.setError();
                                n.setServicio(servicioTranscurrido);
                                n.setRespuesta(n.getRespuesta() + 1);
                                terminados.Add(n);
                            }
                        }
                        else
                        {
                            tres--;
                            n.setFinalizacion(conto);
                            n.setServicio(n.getTime());
                            n.resolver();
                            terminados.Add(n);
                            
                            listBox2.Items.Add(n.getID() + " \t" + n.getOpe() + "\t\t" + n.getResult().ToString());
                            
                        }
                    }
                    sig++;
                    aux.RemoveAt(0);
                    if (listBox2.Items.Count == cantidad)
                    {
                        break;
                    }
                }
                lista.RemoveAt(0);
            }
            labelID.Text = "";
            labelOpe.Text = "";
            labelTme.Text = "";
            labelTt.Text = "";
            labelTr.Text = "";
            all.Clear();
            foreach (Proceso v in terminados)
            {
                v.setEstado(3);
                all.Add(v);
            }
            bcp = new BCP(terminados);
            bcp.ShowDialog();
        }

        private void main_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c;
            BCP bcp;
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
                case 'n':
                    nuevo = true;
                    agregaProceso(cantidad);
                    cantidad++;
                    rest++;
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
                case 't':
                    pause = true;
                    /*while (hilo.ThreadState!=ThreadState.WaitSleepJoin)
                    {
                        Console.WriteLine("g");
                    }*/
                    recolecta();
                    bcp = new BCP(all);
                    bcp.ShowDialog();
                    break;
            }
        }
    }
}
