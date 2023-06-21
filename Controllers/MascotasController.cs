using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
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
    [Authorize]
    public async Task<IActionResult> Get()
    {
        try
        {
            var usuario = User.Identity.Name;
            var mascotas = await contexto.Clientes_Mascotas.Include(x => x.mascota).Include(x => x.cliente).Where(x => x.cliente.authId == usuario).ToListAsync();
            return Ok(mascotas);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("nueva")]
    [Authorize]
    public async Task<IActionResult> nuevaMascota([FromBody] Mascota mascota)
    {
        try
        {
            var usuario = User.Identity.Name;
            var cliente = await contexto.Clientes.FirstOrDefaultAsync(x => x.authId == usuario);
            var activo = 1;
            var resultado = contexto.Mascotas.Add(mascota);
            await contexto.SaveChangesAsync();
            if (resultado.Entity.id != 0)
            {
                contexto.Clientes_Mascotas.Add(new Cliente_mascota { mascotaId = resultado.Entity.id, clienteId = cliente.id, activo = activo });
                await contexto.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }
            return Ok(mascota);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Actualizar([FromBody] Mascota mascota)
    {
        try
        {
            var usuario = User.Identity.Name;
            var mascotaEncontrada = contexto.Mascotas.FirstOrDefault(x => x.id == mascota.id);
            var esDelCliente = await contexto.Clientes_Mascotas.AnyAsync(x => x.mascotaId == mascota.id && x.cliente.authId == usuario);
            if (mascotaEncontrada != null && esDelCliente)
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
            Mascota mascota = contexto.Mascotas.FirstOrDefault(x => x.id == id);
            if (mascota != null)
            {
                mascota.foto = imagePath2;
                contexto.Mascotas.Update(mascota);
                await contexto.SaveChangesAsync();
            }
            return Ok(mascota);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
