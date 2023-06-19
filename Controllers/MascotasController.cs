using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class MascotasController : Controller
{
    private readonly DataContext contexto;

    public MascotasController(DataContext contexto)
    {
        this.contexto = contexto;
    }

    // GET: api/<controller>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(contexto.Mascotas.ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("nueva")]
    [AllowAnonymous]
    public async Task<IActionResult> nuevaMascota([FromBody] Mascota mascota)
    {
        try
        {
            var idCliente = 5;
            var activo = 1;
            var resultado = contexto.Mascotas.Add(mascota);
            await contexto.SaveChangesAsync();
            if (resultado.Entity.id != 0)
            {
                contexto.Clientes_Mascotas.Add(new Cliente_mascota { mascotaId = resultado.Entity.id, clienteId = idCliente, activo = activo });
                await contexto.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Actualizar([FromBody] Mascota mascota)
    {
        try
        {
            var mascotaEncontrada = contexto.Mascotas.FirstOrDefault(x => x.id == mascota.id);
            if (mascotaEncontrada != null)
            {
                mascotaEncontrada.nombre = mascota.nombre;
                mascotaEncontrada.apellido = mascota.apellido;
                mascotaEncontrada.peso = mascota.peso;
                mascotaEncontrada.fechaNacimiento = mascota.fechaNacimiento;
                contexto.Mascotas.Update(mascotaEncontrada);
                await contexto.SaveChangesAsync();
            }
            return Ok(mascotaEncontrada);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
