using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Empleados")]
public class Empleado
{
    public int id { get; set; }
    public String? nombre { get; set; }
    public String? apellido { get; set; }
    public String? mail { get; set; }
    public String? pass { get; set; }
    public String? telefono { get; set; }
    public int? activo { get; set; }

    [ForeignKey(nameof(rolId))]
    public int? rolId { get; set; }

    [ForeignKey(nameof(sucursalId))]
    public int? sucursalId { get; set; }
    public Rol? rol { get; set; }
    public Sucursal? scursal { get; set; }

    public Empleado()
    {

    }
    public Empleado(int id, string? nombre, string? apellido, string? mail, string? pass, string? telefono, int? activo, int? rolId, int? sucursalId, Rol rol, Sucursal scursal)
    {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.mail = mail;
        this.pass = pass;
        this.telefono = telefono;
        this.activo = activo;
        this.rolId = rolId;
        this.sucursalId = sucursalId;
        this.rol = rol;
        this.scursal = scursal;
    }
}