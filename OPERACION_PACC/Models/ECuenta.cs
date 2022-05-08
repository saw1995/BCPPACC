using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPERACION_PACC.Models
{
    public class ECuenta
    {
        public string nro_cuenta { get; set;}
        public string tipo { get; set; }
        public string moneda { get; set; }
        public string nombre { get; set; }
        public decimal saldo { get; set; }
    }

    public class ECuentaLista :ERespuesta
    {
        public List<ECuenta> datos { get; set; }
    }
}
