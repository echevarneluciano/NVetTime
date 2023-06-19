using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ClientesMascotasController : Controller
{
    private readonly DataContext contexto;

    public ClientesMascotasController(DataContext contexto)
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
            return Ok(contexto.Clientes_Mascotas.Include(c => c.cliente).Include(c => c.mascota).ToList().Where(c => c.cliente.authId == usuario));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
