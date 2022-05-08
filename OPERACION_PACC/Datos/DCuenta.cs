using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPERACION_PACC.Models;
using Microsoft.Data.SqlClient;

namespace OPERACION_PACC.Datos
{
    public class DCuenta
    {
        Conexion obj = new Conexion();
        public async Task<ERespuesta> adicionarCuenta(string nroCuenta, string tipo, string moneda, string nombre)
        {
            var result = new ERespuesta();
            result.estado = 0; result.mensaje = "No se ejecuto la sentencia";
            result.tipo = "sql";

            using (var con = new SqlConnection(obj.cn()))
            {
                await con.OpenAsync();

                string sql = "INSERT INTO cuenta(nro_cuenta, tipo, moneda, nombre, saldo) "
                    + "VALUES(@nro_cuenta, @tipo, @moneda, @nombre, 0)";

                using(var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@nro_cuenta", nroCuenta);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@moneda", moneda);
                    cmd.Parameters.AddWithValue("@nombre", nombre);

                    try
                    {
                        int n = await cmd.ExecuteNonQueryAsync();

                        if(n == 1)
                        {
                            result.estado = 1;
                            result.mensaje = "fila afectada o agregado";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.estado = 0;
                        result.mensaje = ex.Message.ToString();
                    }
                }
            }

            return result;
        }

        public async Task<ECuenta> cuentaById(string id)
        {
            var result = new ECuenta();

            using (var con = new SqlConnection(obj.cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT * FROM cuenta WHERE nro_cuenta = @nro";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@nro", id);
                    try
                    {
                        using (var drd = await cmd.ExecuteReaderAsync())
                        {

                            var cuenta = new ECuenta();
                            while (await drd.ReadAsync())
                            {
                                
                                cuenta.nro_cuenta = Convert.ToString(drd["nro_cuenta"]);
                                cuenta.tipo = Convert.ToString(drd["tipo"]);
                                cuenta.moneda = Convert.ToString(drd["moneda"]);
                                cuenta.nombre = Convert.ToString(drd["nombre"]);
                                cuenta.saldo = Convert.ToDecimal(drd["saldo"]);
                            }

                            result = cuenta;
                        }
                    }
                    catch
                    {
                        result = null;
                    }
                }
            }

            return result;
        }
        public async Task<ERespuesta> agregarMovimiento(string nroCuenta, DateTime fecha, string tipo, double importe)
        {
            var result = new ERespuesta();
            result.estado = 0; result.mensaje = "No se ejecuto la sentencia";
            result.tipo = "sql";

            using (var con = new SqlConnection(obj.cn()))
            {
                await con.OpenAsync();

                string sql = "INSERT INTO movimiento(nro_cuenta, fecha, tipo, importe) "
                    + "VALUES(@nro_cuenta, @fecha, @tipo, @importe)";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@nro_cuenta", nroCuenta);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@importe", importe);

                    try
                    {
                        int n = await cmd.ExecuteNonQueryAsync();

                        if (n == 1)
                        {
                            result.estado = 1;
                            result.mensaje = "fila afectada o agregado";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.estado = 0;
                        result.mensaje = ex.Message.ToString();
                    }
                }
            }

            return result;
        }


        public async Task<ERespuesta> updateMovimiento(string nroCuenta, double importe)
        {
            var result = new ERespuesta();
            result.estado = 0; result.mensaje = "No se ejecuto la sentencia";
            result.tipo = "sql";

            using (var con = new SqlConnection(obj.cn()))
            {
                await con.OpenAsync();

                string sql = "UPDATE cuenta SET saldo = @saldo WHERE nro_cuenta=@nro_cuenta";

                using (var cmd = new SqlCommand(sql, con))
                {
                    
                    cmd.Parameters.AddWithValue("@saldo", importe);
                    cmd.Parameters.AddWithValue("@nro_cuenta", nroCuenta);

                    try
                    {
                        int n = await cmd.ExecuteNonQueryAsync();

                        if (n == 1)
                        {
                            result.estado = 1;
                            result.mensaje = "fila afectada o agregado";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.estado = 0;
                        result.mensaje = ex.Message.ToString();
                    }
                }
            }

            return result;
        }
        public async Task<ECuentaLista> listaCuentas()
        {
            var result = new ECuentaLista();
            result.estado = 0; result.mensaje = "No se ejecuto la sentencia";
            result.tipo = "sql";

            using (var con = new SqlConnection(obj.cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT * FROM cuenta ORDER BY nro_cuenta ASC;";

                using (var cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        using(var drd = await cmd.ExecuteReaderAsync())
                        {

                            result.estado = 1;
                            result.mensaje = "sql ejecutado con exito";


                            var listaCuenta = new List<ECuenta>();

                            while(await drd.ReadAsync())
                            {
                                var cuenta = new ECuenta();
                                cuenta.nro_cuenta = Convert.ToString(drd["nro_cuenta"]);
                                cuenta.tipo = Convert.ToString(drd["tipo"]);
                                cuenta.moneda = Convert.ToString(drd["moneda"]);
                                cuenta.nombre = Convert.ToString(drd["nombre"]);
                                cuenta.saldo = Convert.ToDecimal(drd["saldo"]);

                                listaCuenta.Add(cuenta);
                            }

                            result.datos = listaCuenta;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.estado = 0;
                        result.mensaje = ex.Message.ToString();
                    }
                }
            }

            return result;
        }

        public async Task<EmovimientoByCuenta> listaMovimientoByCuenta(string nroCuenta)
        {
            var result = new EmovimientoByCuenta();
            result.estado = 0; result.mensaje = "No se ejecuto la sentencia";
            result.tipo = "sql";

            using (var con = new SqlConnection(obj.cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT tipo, importe, fecha, nro_cuenta FROM movimiento where nro_cuenta=@nro ORDER BY fecha ASC ";

                using (var cmd = new SqlCommand(sql, con))
                {

                    cmd.Parameters.AddWithValue("@nro", nroCuenta);
                    try
                    {
                        using (var drd = await cmd.ExecuteReaderAsync())
                        {

                            result.estado = 1;
                            result.mensaje = "sql ejecutado con exito";


                            var listaMovimiento = new List<EMovimiento>();

                            while (await drd.ReadAsync())
                            {
                                var cuenta = new EMovimiento();
                                cuenta.nro_cuenta = Convert.ToString(drd["nro_cuenta"]);
                                cuenta.tipo = Convert.ToString(drd["tipo"]);
                                cuenta.fecha = drd["fecha"].ToString();
                                cuenta.importe = Convert.ToDouble(drd["importe"]);


                                listaMovimiento.Add(cuenta);
                            }

                            result.datos = listaMovimiento;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.estado = 0;
                        result.mensaje = ex.Message.ToString();
                    }
                }
            }

            return result;
        }

    }
}
