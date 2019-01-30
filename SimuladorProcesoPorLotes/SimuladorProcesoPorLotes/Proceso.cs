using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorProcesoPorLotes
{
    class Proceso
    {
        string name;
        string id;
        string ope;
        int time;
        int result;
        public Proceso() {}
        public Proceso(string n, string i, string o, int t)
        {
            name = n;
            id = i;
            ope = o;
            time = t;
        }
        public string getName()
        {
            return name;
        }
        public string getID()
        {
            return id;
        }
        public string getOpe()
        {
            return ope;
        }
        public int getTime()
        {
            return time;
        }
        public int getResult()
        {
            return result;
        }
        public bool esValido()
        {
            int cont = 0;
            bool sim=false,seg=false;
            foreach (char c in ope)
            {
                if ((c < '0' || c > '9') && cont == 0)
                {
                    return false;
                }
                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '%')
                {
                    sim = true;
                }
                if (c < '0' || c > '9')
                {
                    if (c != '+' && c != '-' && c != '*' && c != '/' && c != '%')
                    {
                        return false;
                    }
                }
                else
                {
                    if (sim)
                    {
                        seg = true;
                    }
                }
                cont++;
            }
            if (seg && sim)
            {
                return true;
            }
            return false;
        }
    }
}
