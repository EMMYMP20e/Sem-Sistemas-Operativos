using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorProcesoPorLotes
{
    public class Proceso
    {
        string id;
        string ope;
        int time;
        int result;
        int transcurrido;
        int llegada;
        int finalizacion;
        int respuesta;
        int bloqueado;
        int servicio;
        bool error=false;
        bool primera = false;

        public Proceso() {}
        public Proceso(string i, string o, int t)
        {
            id = i;
            ope = o;
            time = t;
            transcurrido = 0;
            llegada = 0;
            finalizacion = 0;
            respuesta = 0;
            bloqueado = -1;
            servicio = 0;

        }
        public void setPrimera()
        {
            primera = true;
        }
        public bool getPrimera()
        {
            return primera;
        }
        public void setServicio(int s)
        {
            servicio = s;
        }
        public int getServicio()
        {
            return servicio;
        }
        public void setError()
        {
            error = true;
        }
        public bool getError()
        {
            return error;
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
        public int getTrans()
        {
            return transcurrido;
        }
        public int getLlegada()
        {
            return llegada;
        }
        public int getFinalizacion()
        {
            return finalizacion;
        }
        public int getRespuesta()
        {
            return respuesta;
        }
        public int getBloqueado()
        {
            return bloqueado;
        }

        public void setTrans(int t)
        {
            transcurrido = t;
        }
        public void setLlegada(int l)
        {
            llegada = l;
        }
        public void setFinalizacion(int f)
        {
            finalizacion = f;
        }
        public void setRespuesta(int r)
        {
            respuesta = r;
        }
        public void setBloqueado(int b)
        {
            bloqueado = b;
        }

        public bool esValido()
        {
            int state = 0;
            bool error = false;
            foreach (char c in ope)
            {
                switch (state)
                {
                    case 0:
                        if (c >= '0' && c <= '9')
                        {
                            state = 1;
                        }
                        else
                        {
                            error = true;
                        }
                        break;
                    case 1:
                        if (c >= '0' && c <= '9')
                        {
                            state = 1;
                        }
                        else if (c == '+' || c == '-' || c == '*')
                        {
                            state = 2;
                        }
                        else if (c == '%' || c == '/')
                        {
                            state = 3;
                        }
                        else
                        {
                            error = true;
                        }
                        break;
                    case 2:
                        if (c >= '0' && c <= '9')
                        {
                            state = 4;
                        }
                        else
                        {
                            error = true;
                        }
                        break;
                    case 3:
                        if (c >= '1' && c <= '9')
                        {
                            state = 4;
                        }
                        else
                        {
                            error = true;
                        }
                        break;
                    case 4:
                        if (c >= '0' && c <= '9')
                        {
                            state = 4;
                        }
                        else
                        {
                            error = true;
                        }
                        break;
                }
                if (error)
                {
                    return false;
                }
            }
            if (state == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
            return true;
        }
        public void resolver()
        {
            string uno="", dos="";
            int one, two;
            bool sim=false,sum = false, res = false, mul = false, div = false, mod = false;
            foreach (char c in ope)
            {
                switch (c)
                {
                    case '+':
                        sum = true;
                        sim = true;
                        break;
                    case '-':
                        res = true;
                        sim = true;
                        break;
                    case '*':
                        mul = true;
                        sim = true;
                        break;
                    case '/':
                        div = true;
                        sim = true;
                        break;
                    case '%':
                        mod = true;
                        sim = true;
                        break;
                    default:
                        if (sim)
                        {
                            dos += c;
                        }
                        else
                        {
                            uno += c;
                        }
                        break;
                }
            }
            
            one = Int32.Parse(uno);
            two = Int32.Parse(dos);
            if (sum)
            {
                result = one + two;
            }
            if (res)
            {
                result = one - two;
            }
            if (mul)
            {
                result = one * two;
            }
            if (div)
            {
                result = one / two;
            }
            if (mod)
            {
                result = one % two;
            }
        }
    }
}
