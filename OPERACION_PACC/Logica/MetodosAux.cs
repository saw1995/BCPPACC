using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPERACION_PACC.Logica
{
    public class MetodosAux
    {
        public bool esNumero(string cadena)
        {
            try
            {
                int valor = Convert.ToInt32(cadena);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
