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
                Cliente nuevoCliente = new Cliente();
                nuevoCliente.nombre = cliente.nombre;
                nuevoCliente.apellido = cliente.apellido;
                nuevoCliente.telefono = cliente.telefono;
                nuevoCliente.direccion = cliente.direccion;
                nuevoCliente.mail = cliente.mail;
                nuevoCliente.activo = cliente.activo;
                nuevoCliente.authId = cliente.authId;
                nuevoCliente.foto = "/images/Figura_defecto.png";
                contexto.Clientes.Update(nuevoCliente);
                await contexto.SaveChangesAsync();
                return Ok(nuevoCliente);
            }
            return Ok(clienteEncontrado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("imgupload")]
    [Authorize]
    public async Task<IActionResult> imgUpload([FromForm] String name, [FromForm] String image)
    {
        try
        {
            string imagePath = Path.Combine("wwwroot/images", name + ".jpg");
            string imagePath2 = Path.Combine("/images", name + ".jpg");
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await fileStream.WriteAsync(Convert.FromBase64String(image));
            }
            Cliente cliente = contexto.Clientes.FirstOrDefault(x => x.authId == User.Identity.Name);
            if (cliente != null)
            {
                cliente.foto = imagePath2;
                contexto.Clientes.Update(cliente);
                await contexto.SaveChangesAsync();
            }
            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
