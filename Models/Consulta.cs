using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Consultas")]
public class Consulta
{
    public int id { get; set; }
    public int? estado { get; set; }
    public DateTime? tiempoInicio { get; set; }
    public DateTime? tiempoFin { get; set; }

    [ForeignKey(nameof(empleadoId))]
    public int? empleadoId { get; set; }

    [ForeignKey(nameof(cliente_mascotaId))]
    public int? cliente_mascotaId { get; set; }
    public String? detalle { get; set; }
    public int? activo { get; set; }
    public Empleado? empleado { get; set; }
    public Cliente_mascota? cliente_mascota { get; set; }

    public Consulta()
    {

    }
    public Consulta(int id, int? estado, DateTime? tiempoInicio, DateTime? tiempoFin, int? empleadoId, int? cliente_mascotaId, String? detalle, int? activo, Empleado? empleado, Cliente_mascota? cliente_mascota)
    {
        this.id = id;
        this.estado = estado;
        this.tiempoInicio = tiempoInicio;
        this.tiempoFin = tiempoFin;
        this.empleadoId = empleadoId;
        this.cliente_mascotaId = cliente_mascotaId;
        this.detalle = detalle;
        this.activo = activo;
        this.empleado = empleado;
        this.cliente_mascota = cliente_mascota;
    }
}