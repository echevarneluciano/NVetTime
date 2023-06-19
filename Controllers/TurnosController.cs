using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TurnosController : Controller
{
    private readonly DataContext contexto;

    public TurnosController(DataContext contexto)
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
            var usuario = User.Identity;
            return Ok(contexto.Turnos.Include(x => x.empleado).ToList());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
