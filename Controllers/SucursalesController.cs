using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SucursalesController : Controller
{
    private readonly DataContext contexto;

    public SucursalesController(DataContext contexto)
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
            return Ok(contexto.Sucursales.FindAsync(1).Result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
