using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ConsultasController : Controller
{
    private readonly DataContext contexto;

    public ConsultasController(DataContext contexto)
    {
        this.contexto = contexto;
    }

    // GET: api/<controller>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(contexto.Consultas.Include(x => x.empleado)
            .Include(x => x.cliente_mascota.cliente)
            .Include(x => x.cliente_mascota.mascota)
            .ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{empleado}")]
    [Authorize]
    public async Task<IActionResult> nuevaConsulta([FromBody] Consulta c, string empleado)
    {
        try
        {
            string[] partes = empleado.Split(' ');
            string nombre = partes[0];
            string apellido = partes[1];
            DateTime inicio = (DateTime)c.tiempoInicio;
            var inicioC = inicio.ToString("yyyy/MM/dd HH:mm:ss");
            DateTime fin = (DateTime)c.tiempoFin;
            var finC = fin.ToString("yyyy/MM/dd HH:mm:ss");
            Consulta item = new Consulta();

            using (var command = contexto.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @$"SELECT * FROM consultas
            WHERE ('{inicioC}' > consultas.tiempoinicio AND '{inicioC}' < consultas.tiempofin)
            OR ('{finC}' > consultas.tiempoinicio AND '{finC}' < consultas.tiempofin)
            OR ('{inicioC}' <= consultas.tiempoinicio AND '{finC}' >= consultas.tiempofin)
            AND consultas.empleadoid = {c.empleadoId};";
                contexto.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        item.id = result.GetInt32(0);
                    }
                }
            }
            if (item.id == 0)
            {
                using (var command = contexto.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @$"INSERT INTO consultas 
                (empleadoid, tiempoinicio, tiempofin, cliente_mascotaid, estado, detalle)
                SELECT e.id, '{inicioC}', '{finC}', {c.cliente_mascotaId}, 1, '{c.detalle}'
                FROM empleados e
                WHERE e.nombre = '{nombre}' AND e.apellido = '{apellido}';
                SELECT LAST_INSERT_ID();";
                    contexto.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            item.id = result.GetInt32(0);
                        }
                    }
                }
                var consultaCreada = contexto.Consultas.Find(item.id);
                return Ok(consultaCreada);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("turnos/{fecha}/{empleado}")]
    [Authorize]
    public async Task<IActionResult> turnosOcupados(string fecha, string empleado)
    {
        try
        {
            string[] partes = empleado.Split(' ');
            string nombre = partes[0];
            string apellido = partes[1];
            List<Consulta> Consultas = new List<Consulta>();
            using (var command = contexto.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @$"select c.id, c.tiempoinicio, c.tiempofin, c.cliente_mascotaid, c.estado, c.detalle
                from consultas c
                JOIN empleados e ON e.id = c.empleadoid
                where DATE(tiempoInicio) = '{fecha}'
                AND	e.nombre = '{nombre}' AND e.apellido = '{apellido}';";
                contexto.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        Consulta item = new Consulta();
                        item.id = result.GetInt32(0);
                        item.tiempoInicio = result.GetDateTime(1);
                        item.tiempoFin = result.GetDateTime(2);
                        item.cliente_mascotaId = result.GetInt32(3);
                        item.estado = result.GetInt32(4);
                        item.detalle = result.GetString(5);

                        Consultas.Add(item);
                    }
                }
            }

            return Ok(Consultas);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("pendientes")]
    [Authorize]
    public async Task<IActionResult> GetPendientes()
    {
        try
        {
            var usuario = User.Identity.Name;
            var pendientes = contexto.Consultas.Where(x => x.estado == 1).Include(x => x.cliente_mascota).ThenInclude(x => x.mascota).Include(x => x.empleado).Where(x => x.cliente_mascota.cliente.authId == usuario).ToList();
            return Ok(pendientes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("historial")]
    [Authorize]
    public async Task<IActionResult> GetHistorial()
    {
        try
        {
            var usuario = User.Identity.Name;
            var pendientes = contexto.Consultas.Where(x => x.estado == 0).Include(x => x.cliente_mascota).ThenInclude(x => x.mascota).Include(x => x.empleado).Where(x => x.cliente_mascota.cliente.authId == usuario).Take(10).ToList();
            return Ok(pendientes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("imgupload")]
    [Authorize]
    public async Task<IActionResult> imgUpload([FromForm] String name, [FromForm] String image, [FromForm] Int16 id)
    {
        try
        {
            string imagePath = Path.Combine("wwwroot/images", name + ".jpg");
            string imagePath2 = Path.Combine("/images", name + ".jpg");
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await fileStream.WriteAsync(Convert.FromBase64String(image));
            }
            Consulta consulta = contexto.Consultas.FirstOrDefault(x => x.id == id);
            if (consulta != null)
            {
                consulta.foto = imagePath2;
                contexto.Consultas.Update(consulta);
                await contexto.SaveChangesAsync();
            }
            return Ok(consulta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
