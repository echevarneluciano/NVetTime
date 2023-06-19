using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class ClientesController : Controller
{
    private readonly DataContext contexto;

    public ClientesController(DataContext contexto)
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
            return Ok(contexto.Clientes.FindAsync(5).Result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Actualizar([FromBody] Cliente cliente)
    {
        try
        {
            var clienteEncontrado = contexto.Clientes.FirstOrDefault(x => x.id == cliente.id);
            if (clienteEncontrado != null)
            {
                clienteEncontrado.nombre = cliente.nombre;
                clienteEncontrado.apellido = cliente.apellido;
                clienteEncontrado.telefono = cliente.telefono;
                clienteEncontrado.direccion = cliente.direccion;
                clienteEncontrado.mail = cliente.mail;
                clienteEncontrado.activo = 1;
                contexto.Clientes.Update(clienteEncontrado);
                await contexto.SaveChangesAsync();
            }
            return Ok(clienteEncontrado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
