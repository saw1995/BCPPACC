using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPERACION_PACC.Datos;
using OPERACION_PACC.Models;

namespace OPERACION_PACC.Logica
{
    public class LCuenta
    {
        public async Task<ERespuesta> adicionarNuevaCuenta(string nroCuenta, string moneda, string nombre)
        {
            var result = new ERespuesta();
            result.estado = 0; result.tipo = "logica";
            result.mensaje = "No se ejecuto la funcion";

            string tipoCuenta;
            if (new MetodosAux().esNumero(nroCuenta))
            {
                tipoCuenta = "";
            }
            else if (nroCuenta.Length == 13)
            {
                tipoCuenta = "CTE";
            }
            else if (nroCuenta.Length == 14)
            {
                tipoCuenta = "AHO";
            }
            else
            {
                tipoCuenta = "";
            }

            string tipoMoneda;
            if (moneda.Equals("Bolivianos"))
            {
                tipoMoneda = "BOL";
            }
            else if (moneda == "Dolares")
            {
                tipoMoneda = "USD";
            }
            else { tipoMoneda = ""; }


            if (!tipoCuenta.Equals("") && !tipoMoneda.Equals("") && !nombre.Equals(""))
            {
                result = await new DCuenta().adicionarCuenta(nroCuenta, tipoCuenta, tipoMoneda, nombre);
            }
            else
            {
                result.estado = 0;
                result.mensaje = "No puede enviar valores vacios en los campos numero cuenta, moneda o nombre";
            }

            return result;
        }

        public async Task<ECuentaLista> listaCuentas()
        {
            return await new DCuenta().listaCuentas();
        }

        public async Task<EmovimientoByCuenta> listaMovimientoByCuenta(string nroCuenta)
        {
            return await new DCuenta().listaMovimientoByCuenta(nroCuenta);
        }

        public async Task<ERespuesta> agregarMovimiento(string nroCuenta, string tipo, double importe, double saldoActual)
        {
            var result = new ERespuesta();
            result.estado = 0; result.tipo = "logica";
            result.mensaje = "No se ejecuto la funcion";

            DateTime fecha = DateTime.Now;
            var nuevoSaldo = 0.0;
            if (tipo.Equals("A"))
            {
                nuevoSaldo = saldoActual - importe;
            }

            if (tipo.Equals("D"))
            {
                nuevoSaldo = saldoActual + importe;
               
            }
            result = await new DCuenta().agregarMovimiento(nroCuenta, fecha, tipo, importe);
            if (result.estado == 1)
            {
                result = await new DCuenta().updateMovimiento(nroCuenta, nuevoSaldo);
            }

            return result;
        }

        public async Task<ERespuesta> TransferenciaMonto(string nroCuentaEmisor, string nroCuentaReceptor, double importe)
        {
            var result = new ERespuesta();
            result.estado = 0; result.tipo = "logica";
            result.mensaje = "No se ejecuto la funcion";

            DateTime fecha = DateTime.Now;
            var nuevoSaldo = 0.0;

            var oEmisor = await new DCuenta().cuentaById(nroCuentaEmisor);
            result = await new DCuenta().agregarMovimiento(nroCuentaEmisor, fecha, "A", importe);
            nuevoSaldo = Double.Parse(oEmisor.saldo.ToString()) - importe;
            result = await new DCuenta().updateMovimiento(nroCuentaEmisor, nuevoSaldo);

            var oReceptor = await new DCuenta().cuentaById(nroCuentaReceptor);
            result = await new DCuenta().agregarMovimiento(nroCuentaReceptor, fecha, "D", importe);
            nuevoSaldo = Double.Parse(oReceptor.saldo.ToString()) + importe;
            result = await new DCuenta().updateMovimiento(nroCuentaReceptor, nuevoSaldo);

            return result;
        }


    }
}
