using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPERACION_PACC.Logica;
using OPERACION_PACC.Models;

namespace OPERACION_PACC.Controllers
{
    public class CuentasController : Controller
    {
        public IActionResult AdicionarCuentas()
        {
            /*var result = await new Logica.LCuenta().adicionarCuenta(numero, moneda, nombre);
            ViewBag.Message = result.mensaje;
            ViewData["miMensaje"] = result.mensaje;
            return View(result);*/
            return View();
        }

        [HttpPost]
        public async Task<ERespuesta> agregarCuenta(string numero, string moneda, string nombre)
        {
            var result = await new Logica.LCuenta().adicionarNuevaCuenta(numero, moneda, nombre);
            return result;
        }

        [HttpPost]
        public async Task<ECuentaLista> listaCuentas()
        {
            var result = await new Logica.LCuenta().listaCuentas();
            return result;
        }

        [HttpPost]
        public async Task<ERespuesta> agregarMovimiento(string nro_cuenta, string tipo, double importe, double saldoActual)
        {
            var result = await new Logica.LCuenta().agregarMovimiento(nro_cuenta, tipo, importe, saldoActual);
            return result;
        }

        [HttpPost]
        public async Task<ERespuesta> transferenciaMonto(string nro_cuenta_emisor, string nro_cuenta_receptor, double importe)
        {
            var result = await new Logica.LCuenta().TransferenciaMonto(nro_cuenta_emisor, nro_cuenta_receptor, importe);
            return result;
        }

        [HttpPost]
        public async Task<EmovimientoByCuenta> listaMovimientoByCuenta(string nro_cuenta)
        {
            var result = await new Logica.LCuenta().listaMovimientoByCuenta(nro_cuenta);
            return result;
        }

        public IActionResult AbonosCuentas()
        {
            return View();
        }

        public IActionResult transferenciasEntreCuentas()
        {
            return View();
        }

        public IActionResult MovimientosCuenta()
        {
            return View();
        }

        public IActionResult movimientosByCuenta(string idCuenta)
        {
            ViewBag.idCuenta = idCuenta;
            return View();
        }
    }
}