using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

using RESTAPI_CORE.Modelos;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace RESTAPI_CORE.Controllers
{

    public class ProductoController : ControllerBase
    {
        private readonly string cadenaSQL;
        public ProductoController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }


        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Repository> lista = new List<Repository>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_productos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Repository
                            {
                                Id = rd["id"].ToString(),
                                Nombre = rd["Nombre"].ToString(),
                                Precio = rd["Precio"].ToString(),
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, JsonConvert.SerializeObject(lista));
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });

            }
        }




        // obtener por id

        [HttpGet]
        //[Route("Obtener")] // => Obtener?idProducto=13 | ampersand
        [Route("Obtener/{id}")]
        public IActionResult Obtener(string id)
        {
            List<Repository> lista = new List<Repository>();
            Repository oProducto = new Repository();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_productos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Repository
                            {
                                Id = rd["Id"].ToString(),
                                Nombre = rd["Nombre"].ToString(),
                                Precio = rd["Precio"].ToString(),
                            });
                        }

                    }
                }

                oProducto = lista.Where(item => item.Id == id).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProducto });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = oProducto });

            }
        }
        // guardar


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Repository objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_producto", conexion);
                    cmd.Parameters.AddWithValue("id", objeto.Id);
                    cmd.Parameters.AddWithValue("nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("precio", objeto.Precio);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "agregado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Repository objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_producto", conexion);
                    cmd.Parameters.AddWithValue("id", objeto.Id is null ? DBNull.Value : objeto.Id);
                    cmd.Parameters.AddWithValue("nombre", objeto.Nombre is null ? DBNull.Value : objeto.Nombre);
                    cmd.Parameters.AddWithValue("precio", objeto.Precio is null ? DBNull.Value : objeto.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        // eliminar

        [HttpDelete]
        [Route("Eliminar/{producto}")]
        public IActionResult Eliminar(string producto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_producto", conexion);
                    cmd.Parameters.AddWithValue("producto", producto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }




    }
}
