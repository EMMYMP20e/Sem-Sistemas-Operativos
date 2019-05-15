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
using System.IO;
using System.Text;

namespace SimuladorProcesoPorLotes
{
    public partial class main : Form
    {
        List<Proceso> lista;
        int cantidad;
        int quantum;
        Thread hilo;
        bool interrupt = false, error = false, pause = false, nuevo = false, mostrar = false, suspender = false, regresa=false;
        Random r = new Random(DateTime.Now.Millisecond);
        string[] opes = { "+", "-", "/", "%", "*" };
        int rest;
        String auxFile="C:\\Users\\Emmanualto\\Desktop\\CUCEI\\Sem. Sistemas Operativos\\Sem-Sistemas-Operativos\\08-ProcesosSuspendidos\\Auxiliar.txt";
        String suspendedFile = "C:\\Users\\Emmanualto\\Desktop\\CUCEI\\Sem. Sistemas Operativos\\Sem-Sistemas-Operativos\\08-ProcesosSuspendidos\\Suspendidos.txt";

        List<Proceso> aux;
        List<Proceso> bloqueados;
        List<Proceso> terminados;
        List<Proceso> all= new List<Proceso>();
        public main()
        {
            inicio();
            procesos();
            InitializeComponent();
            cargaTabla();
            CheckForIllegalCrossThreadCalls = false;
            hilo = new Thread(new ThreadStart(procesado));
            simulacion();
        }
        private void cargaTabla()
        {
            ListViewItem id;
            
            for (int i = 0; i <= 35; i++)
            {
                
                id = new ListViewItem(i.ToString());
                if (i % 2 == 0)
                {
                    
                    listView1.Items.Add(id);
                }
                else
                {
                    listView2.Items.Add(id);
                }
            }
            foreach (ListViewItem l in listView1.Items)
            {
                //l.UseItemStyleForSubItems = false;
                for (int i = 0; i < 6; i++)
                {
                    l.SubItems.Add("");
                }
            }
            foreach (ListViewItem l in listView2.Items)
            {
                //l.UseItemStyleForSubItems = false;
                for (int i = 0; i < 6; i++)
                {
                    l.SubItems.Add("");
                }
            }
            for (int j = 0; j < 18; j++)
            {
                listView1.Items[j].UseItemStyleForSubItems = false;
                for (int i = 0; i < 7; i++)
                {
                    listView1.Items[j].SubItems[i].BackColor = Color.White;
                }
            }
            for (int j = 0; j < 18; j++)
            {
                listView2.Items[j].UseItemStyleForSubItems = false;
                for (int i = 0; i < 7; i++)
                {
                    listView2.Items[j].SubItems[i].BackColor = Color.White;
                }
            }
            id = new ListViewItem("0");
            for (int i = 0; i < 6; i++)
            {
                id.SubItems.Add("");
            }
            for (int i = 1; i < 6; i++)
            {
                id.SubItems[i].BackColor = Color.Black;
            }
            id.UseItemStyleForSubItems = false;
            listView1.Items[0] = id;
            id = new ListViewItem("1");
            for (int i = 0; i < 6; i++)
            {
                id.SubItems.Add("");
            }
            for (int i = 1; i < 6; i++)
            {
                id.SubItems[i].BackColor = Color.Black;
            }
            id.UseItemStyleForSubItems = false;
            listView2.Items[0] = id;
            listView1.Items[0].SubItems[6].Text = "SO";
            listView2.Items[0].SubItems[6].Text = "SO";
            /*pintaTabla(12, Color.Blue, 1);
            pintaTabla(13, Color.Purple, 2);
            pintaTabla(15, Color.Yellow, 3);
            //Console.WriteLine(listView1.Items[1].SubItems[1].BackColor);
            if (listView1.Items[1].SubItems[1].BackColor == Color.White)
            {
                Console.WriteLine("gg");
            }*/

        }
        private bool isAvailable(int size)
        {
            int count=0;
            foreach (ListViewItem l in listView1.Items)
            {
                if (l.SubItems[1].BackColor == Color.White)
                {
                    count+=5;
                }
                if (count >= size)
                {
                    return true;
                }
            }
            foreach (ListViewItem l in listView2.Items)
            {
                if (l.SubItems[1].BackColor == Color.White)
                {
                    count += 5;
                }
                if (count >= size)
                {
                    return true;
                }
            }
            return false;
            
        }
        private void pintaTabla(int size, Color color, string id)
        {
            int count = 0;

            foreach (ListViewItem l in listView1.Items)
            {
                if (l.SubItems[1].BackColor == Color.White)
                {
                    int i = 1;
                    while (i < 6 && count != size)
                    {
                        l.SubItems[i].BackColor = color;
                        count++;
                        i++;
                    }
                    l.SubItems[6].Text = id;
                    if (count == size)
                    {
                        return;
                    }
                }
            }
            foreach (ListViewItem l in listView2.Items)
            {
                if (l.SubItems[1].BackColor == Color.White)
                {
                    int i = 1;
                    while (i < 6 && count != size)
                    {
                        l.SubItems[i].BackColor = color;
                        count++;
                        i++;
                    }
                    l.SubItems[6].Text = id;
                    if (count == size)
                    {
                        return;
                    }
                }
            }
        }
        private void limpiaTabla(string id)
        {
            foreach (ListViewItem l in listView1.Items)
            {
                if (l.SubItems[6].Text == id)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        l.SubItems[i].BackColor = Color.White;
                    }
                    l.SubItems[6].Text = "";
                }
            }
            foreach (ListViewItem l in listView2.Items)
            {
                if (l.SubItems[6].Text == id)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        l.SubItems[i].BackColor = Color.White;
                    }
                    l.SubItems[6].Text = "";
                }
            }
        }
        private void ActualizaColor(Color color, string id)
        {
            foreach (ListViewItem l in listView1.Items)
            {
                if (l.SubItems[6].Text == id)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        if (l.SubItems[i].BackColor != Color.White)
                        {
                            l.SubItems[i].BackColor = color;
                        }
                    }
                }
            }
            foreach (ListViewItem l in listView2.Items)
            {
                if (l.SubItems[6].Text == id)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        if (l.SubItems[i].BackColor != Color.White)
                        {
                            l.SubItems[i].BackColor = color;
                        }
                    }
                }
            }
        }
        private void inicio()
        {
            Form1 form = new Form1();
            form.ShowDialog();
            cantidad = form.getCantidad();
            quantum = form.getQuantum();
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
            int rSize = r.Next(6, 30);
            string ecuacion = rUno.ToString() + opes[rOpe] + rDos.ToString();
            Proceso proc = new Proceso((i + 1).ToString(), ecuacion, rTime,rSize);
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
        public void writeToDisk(Proceso p)
        {
            StreamWriter sw = new StreamWriter(suspendedFile, true);
            sw.Write(p.getBloqueado());
            sw.Write('|');
            sw.Write(p.getError());
            sw.Write('|');
            sw.Write(p.getEspera());
            sw.Write('|');
            sw.Write(p.getEstado());
            sw.Write('|');
            sw.Write(p.getFinalizacion());
            sw.Write('|');
            sw.Write(p.getID());
            sw.Write('|');
            sw.Write(p.getLlegada());
            sw.Write('|');
            sw.Write(p.getOpe());
            sw.Write('|');
            sw.Write(p.getPrimera());
            sw.Write('|');
            sw.Write(p.getRespuesta());
            sw.Write('|');
            sw.Write(p.getResult());
            sw.Write('|');
            sw.Write(p.getServicio());
            sw.Write('|');
            sw.Write(p.getSize());
            sw.Write('|');
            sw.Write(p.getTime());
            sw.Write('|');
            sw.Write(p.getTrans());
            sw.Write('\n');
            sw.Close();
        }
        public bool AnySuspended()
        {
            string linea="";
            try
            {
                StreamReader sr = new StreamReader(suspendedFile);
                linea = sr.ReadToEnd();
                sr.Close();
                if (linea == "")
                {
                    return false;
                }
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }
        public Proceso readFromDisk()
        {
            String linea;
            Proceso regresa = new Proceso();
            StreamReader sr = new StreamReader(suspendedFile);
            linea = sr.ReadLine();
            var strs = linea.Split('|');
            regresa.setBloqueado(Int32.Parse(strs[0]));
            if (strs[1] == "True")
            {
                regresa.setError();
            }
            regresa.setEspera(Int32.Parse(strs[2]));
            regresa.setEstado(Int32.Parse(strs[3]));
            regresa.setFinalizacion(Int32.Parse(strs[4]));
            regresa.setID(strs[5]);
            regresa.setLlegada(Int32.Parse(strs[6]));
            regresa.setOpe(strs[7]);
            if (strs[8] == "True")
            {
                regresa.setPrimera();
            }
            regresa.setRespuesta(Int32.Parse(strs[9]));
            regresa.setResult(Int32.Parse(strs[10]));
            regresa.setServicio(Int32.Parse(strs[11]));
            regresa.setSize(Int32.Parse(strs[12]));
            regresa.setTime(Int32.Parse(strs[13]));
            regresa.setTrans(Int32.Parse(strs[14]));
            linea = sr.ReadToEnd();
            /*var rest = linea.Split('\n');
            List<String> listaNueva = rest.OfType<String>().ToList();
            listaNueva.RemoveAt(0);*/
            sr.Close();
            return regresa;
        }
        public void recorreTxt()
        {
            string linea;
            StreamReader sr = new StreamReader(suspendedFile);
            linea = sr.ReadLine();
            linea = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(auxFile, true);
            /*foreach (String l in listaNueva)
            {
                sw.Write(l);
                sw.Write('\n');
                Console.WriteLine("lineas");
            }*/
            sw.Write(linea);
            sw.Close();
            File.Delete(suspendedFile);
            File.Copy(auxFile, suspendedFile);
            File.Delete(auxFile);
        }
        public void procesado()
        {
            int cont = 0;
            int sig = 0;
            rest = cantidad;
            int total, restante;
            int conto = 0;
            int tme;
            int servicioTranscurrido = 0;
            int quantumActual;
            bool finQuantum = false;
            labelContador.Text = sig.ToString();
            labelPendientes.Text = cantidad.ToString();
            labelQuantum.Text = quantum.ToString();

            BCP bcp;
            aux = new List<Proceso>();
            bloqueados = new List<Proceso>();
            terminados = new List<Proceso>();

            Proceso p;
            Proceso nP;
            Proceso next;
            while (lista.Count != 0 && !AnySuspended())
            {
                ///
                ///
                /*writeToDisk(p);
                Console.WriteLine(AnySuspended());
                if (AnySuspended())
                {
                    if (p.getID() == "5")
                    {
                        readFromDisk();
                    }
                    
                }*/

                ///
                ///
                p = lista.First<Proceso>();
                next = p;
                if (isAvailable(p.getSize()))
                {
                    pintaTabla(p.getSize(), Color.Blue, p.getID());
                    listBox1.Items.Add(p.getID() + "\t\t" + p.getTime() + "\t" + p.getTrans());
                    p.setLlegada(conto);
                    aux.Add(p);
                    cont++;
                    rest--;
                    labelPendientes.Text = rest.ToString();
                    if (p == lista.Last<Proceso>())
                    {
                        labelIDsig.Text = "";
                        labelSize.Text = "";
                    }
                    else
                    {
                        next = lista.ElementAt<Proceso>(1);
                        labelIDsig.Text = next.getID().ToString();
                        labelSize.Text = next.getSize().ToString();
                    }
                }

                
                //while (tres == 3 || cont == cantidad)
                while (!isAvailable(next.getSize()) || cont == cantidad)
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
                        if (suspender)
                        {
                            if (bloqueados.Count > 0)
                            {
                                Proceso first;
                                first = bloqueados.First<Proceso>();
                                limpiaTabla(first.getID());
                                bloqueados.RemoveAt(0);
                                listBox3.Items.RemoveAt(0);
                                writeToDisk(first);
                                if (lista.Count > 1)
                                {
                                    p = lista.ElementAt<Proceso>(1);
                                    next = p;
                                    if (isAvailable(p.getSize()))
                                    {
                                        pintaTabla(p.getSize(), Color.Blue, p.getID());
                                        listBox1.Items.Add(p.getID() + "\t\t" + p.getTime() + "\t" + p.getTrans());
                                        p.setLlegada(conto);
                                        aux.Add(p);
                                        cont++;
                                        rest--;
                                        labelPendientes.Text = rest.ToString();
                                        if (p == lista.Last<Proceso>())
                                        {
                                            labelIDsig.Text = "";
                                            labelSize.Text = "";
                                        }
                                        else
                                        {
                                            next = lista.ElementAt<Proceso>(1);
                                            labelIDsig.Text = next.getID().ToString();
                                            labelSize.Text = next.getSize().ToString();
                                        }
                                        lista.RemoveAt(0);
                                    }
                                }
                            }
                            suspender = false;
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
                                ActualizaColor(Color.Blue, bloqueados.ElementAt<Proceso>(k).getID());///what
                                bloqueados.RemoveAt(k);
                            }
                        }
                        
                        if (regresa)
                        {
                            if (AnySuspended())
                            {
                                Proceso lys;
                                lys = readFromDisk();
                                if (isAvailable(lys.getSize()))
                                {
                                    pintaTabla(lys.getSize(), Color.Blue, lys.getID());
                                    listBox1.Items.Add(lys.getID() + "\t\t" + lys.getTime() + "\t" + lys.getTrans());
                                    aux.Add(lys);
                                    recorreTxt();
                                }
                            }
                            regresa = false;
                        }
                        conto++;
                        labelContador.Text = conto.ToString();
                        
                        if (nuevo)
                        {
                            if (lista.Count>2)
                            {
                                labelPendientes.Text = rest.ToString();
                            }
                            else
                            {
                                nP = lista.Last<Proceso>();
                                next = nP;
                                if (isAvailable(nP.getSize()))
                                {
                                    pintaTabla(nP.getSize(), Color.Blue, nP.getID());
                                    listBox1.Items.Add(nP.getID() + "\t\t" + nP.getTime() + "\t" + nP.getTrans());
                                    nP.setLlegada(conto);
                                    aux.Add(nP);
                                    cont++;
                                    rest--;
                                    lista.RemoveAt(lista.Count - 1);
                                    labelPendientes.Text = rest.ToString();
                                    labelIDsig.Text = "";
                                    labelSize.Text = "";
                                }
                                else
                                {
                                    labelPendientes.Text = rest.ToString();
                                    labelIDsig.Text = next.getID().ToString();
                                    labelSize.Text = next.getSize().ToString();
                                }
                                
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
                    quantumActual = 0;
                    ActualizaColor(Color.Red, n.getID());
                    for (int j = i; j < tme; j++)
                    {
                        n.setServicio(total + 1);
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
                            limpiaTabla(n.getID());
                        }
                        if (interrupt)
                        {
                            n.setTrans(total);
                            j = n.getTime();
                            bloqueados.Add(n);
                            ActualizaColor(Color.Purple, n.getID());
                        }
                        else
                        {
                            quantumActual++;
                            if (j != tme - 1)
                            {
                                if (quantumActual == quantum)
                                {
                                    n.setTrans(total);
                                    j = n.getTime();
                                    listBox1.Items.Add(n.getID() + "\t\t" + n.getTime() + "\t" + n.getTrans());
                                    aux.Add(n);
                                    finQuantum = true;
                                    ActualizaColor(Color.Blue, n.getID());
                                }
                            }
                        }
                        if (suspender)
                        {
                            if (bloqueados.Count > 0)
                            {
                                Proceso first;
                                first = bloqueados.First<Proceso>();
                                limpiaTabla(first.getID());
                                bloqueados.RemoveAt(0);
                                listBox3.Items.RemoveAt(0);
                                writeToDisk(first);
                                if (lista.Count > 1)
                                {
                                    p = lista.ElementAt<Proceso>(1);
                                    next = p;
                                    if (isAvailable(p.getSize()))
                                    {
                                        pintaTabla(p.getSize(), Color.Blue, p.getID());
                                        listBox1.Items.Add(p.getID() + "\t\t" + p.getTime() + "\t" + p.getTrans());
                                        p.setLlegada(conto);
                                        aux.Add(p);
                                        cont++;
                                        rest--;
                                        labelPendientes.Text = rest.ToString();
                                        if (p == lista.Last<Proceso>())
                                        {
                                            labelIDsig.Text = "";
                                            labelSize.Text = "";
                                        }
                                        else
                                        {
                                            //Console.WriteLine("cc");
                                            next = lista.ElementAt<Proceso>(1);
                                            labelIDsig.Text = next.getID().ToString();
                                            labelSize.Text = next.getSize().ToString();
                                        }
                                        lista.RemoveAt(0);
                                    }
                                }
                            }
                            suspender = false;
                        }
                        if (regresa)
                        {
                            if (AnySuspended())
                            {
                                Proceso lys;
                                lys = readFromDisk();
                                if (isAvailable(lys.getSize()))
                                {
                                    pintaTabla(lys.getSize(), Color.Blue, lys.getID());
                                    listBox1.Items.Add(lys.getID() + "\t\t" + lys.getTime() + "\t" + lys.getTrans());
                                    aux.Add(lys);
                                    recorreTxt();
                                }
                            }
                            regresa = false;
                        }
                        if (nuevo)
                        {
                            //Console.WriteLine(lista.Count);
                            if (lista.Count > 2)
                            {
                                labelPendientes.Text = rest.ToString();
                                //Console.WriteLine("g");
                            }
                            else
                            {
                                nP = lista.Last<Proceso>();
                                next = nP;
                                if (isAvailable(nP.getSize()))
                                {
                                    pintaTabla(nP.getSize(), Color.Blue, nP.getID());
                                    listBox1.Items.Add(nP.getID() + "\t\t" + nP.getTime() + "\t" + nP.getTrans());
                                    nP.setLlegada(conto);
                                    aux.Add(nP);
                                    cont++;
                                    rest--;
                                    lista.RemoveAt(lista.Count - 1);
                                    labelPendientes.Text = rest.ToString();
                                    labelIDsig.Text = "";
                                    labelSize.Text = "";
                                }
                                else
                                {
                                    labelPendientes.Text = rest.ToString();
                                    labelIDsig.Text = next.getID().ToString();
                                    labelSize.Text = next.getSize().ToString();
                                }

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
                                ActualizaColor(Color.Blue, bloqueados.ElementAt<Proceso>(k).getID());
                                
                                bloqueados.RemoveAt(k);
                                
                            }
                        }
                        
                        if ((bloqueados.Count + listBox2.Items.Count) == cantidad)
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
                    else if (finQuantum)
                    {
                        finQuantum = false;
                    }
                    else
                    {
                        if (error)
                        {
                            if (bloqueados.Count != 3 && (bloqueados.Count + listBox2.Items.Count) != cantidad)
                            {
                                listBox2.Items.Add(n.getID() + " \t" + n.getOpe() + "\t\tError");
                                //tres--;
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
                            //tres--;
                            n.setFinalizacion(conto);
                            n.setServicio(n.getTime());
                            n.resolver();
                            terminados.Add(n);
                            limpiaTabla(n.getID());
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
                //Console.WriteLine("gg");
            }
            labelID.Text = "";
            labelOpe.Text = "";
            labelTme.Text = "";
            labelTt.Text = "";
            labelTr.Text = "";
            if (all.Count != 0)
            {
                all.Clear();
            }
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
                case 'm':
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
                    recolecta();
                    bcp = new BCP(all);
                    bcp.ShowDialog();
                    break;
                case 's':
                    suspender = true;
                    break;
                case 'r':
                    regresa = true;
                    break;
            }
        }
    }
}
