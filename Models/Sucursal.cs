using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Sucursales")]
public class Sucursal
{
    public int id { get; set; }
    public String? nombre { get; set; }
    public String? direccion { get; set; }
    public String? redSocial { get; set; }
    public String? horario { get; set; }
    public String? telefono { get; set; }
    public int? activo { get; set; }
    public Sucursal()
    {

    }
    public Sucursal(int id, string? nombre, string? direccion, string? redSocial, string? horario, string? telefono, int? activo)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.redSocial = redSocial;
        this.horario = horario;
        this.telefono = telefono;
        this.activo = activo;
    }
}