using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(contexto.Clientes_Mascotas.Include(c => c.cliente).Include(c => c.mascota).ToList().Where(c => c.clienteId == 5));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
