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
    public String? authId { get; set; }
    public String? telefono { get; set; }
    public String? direccion { get; set; }
    public String? foto { get; set; }
    public int? activo { get; set; }

    public Cliente()
    {

    }
    public Cliente(int id, string? nombre, string? apellido, string? mail, string? authId, string? telefono, string? direccion, int? activo, string? foto)
    {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.mail = mail;
        this.authId = authId;
        this.telefono = telefono;
        this.direccion = direccion;
        this.activo = activo;
        this.foto = foto;
    }
}