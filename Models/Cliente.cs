using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Clientes")]
public class Cliente
{
    public int id { get; set; }
    public String? nombre { get; set; }
    public String? apellido { get; set; }
    public String? mail { get; set; }
    public String? pass { get; set; }
    public String? telefono { get; set; }
    public String? direccion { get; set; }
    public int? activo { get; set; }

    public Cliente()
    {

    }
    public Cliente(int id, string? nombre, string? apellido, string? mail, string? pass, string? telefono, string? direccion, int? activo)
    {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.mail = mail;
        this.pass = pass;
        this.telefono = telefono;
        this.direccion = direccion;
        this.activo = activo;
    }
}