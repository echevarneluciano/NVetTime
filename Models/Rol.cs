using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Roles")]
public class Rol
{
    public int id { get; set; }
    public String? rol { get; set; }
    public int? activo { get; set; }

    public Rol()
    {

    }
    public Rol(int id, string? rol, int? activo)
    {
        this.id = id;
        this.rol = rol;
        this.activo = activo;
    }
}