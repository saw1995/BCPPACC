using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPERACION_PACC.Models
{
    public class EMovimiento
    {
        public string nro_cuenta { get; set; }
        public string fecha { get;   set; }
        public string tipo { get; set; }
        public double importe { get; set; }
    }
    public class EmovimientoByCuenta : ERespuesta
    {
        public List<EMovimiento> datos { get; set; }
    }
}
