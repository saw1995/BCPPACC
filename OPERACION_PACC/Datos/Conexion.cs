using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPERACION_PACC.Datos
{
    public class Conexion
    {
        public string cn()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("connectionStrings")["cadena"];
        }
    }
}
