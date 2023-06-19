using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NVetTime.Models;

namespace VetTime.Controllers;


[Route("api/[controller]")]
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
    [Authorize]
    public async Task<IActionResult> Get()
    {
        try
        {
            var usuario = User.Identity.Name;
            return Ok(contexto.Clientes.SingleOrDefault(x => x.authId == usuario));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Actualizar([FromBody] Cliente cliente)
    {
        try
        {
            var usuario = User.Identity.Name;
            var clienteEncontrado = contexto.Clientes.FirstOrDefault(x => x.id == cliente.id && x.authId == usuario);
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

    [HttpPost("login")]
    [Authorize]
    public async Task<IActionResult> login([FromBody] Cliente cliente)
    {
        try
        {
            var clienteEncontrado = contexto.Clientes.FirstOrDefault(x => x.authId == cliente.authId);
            if (clienteEncontrado == null)
            {
                clienteEncontrado.nombre = cliente.nombre;
                clienteEncontrado.apellido = cliente.apellido;
                clienteEncontrado.telefono = cliente.telefono;
                clienteEncontrado.direccion = cliente.direccion;
                clienteEncontrado.mail = cliente.mail;
                clienteEncontrado.activo = cliente.activo;
                clienteEncontrado.authId = cliente.authId;
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
