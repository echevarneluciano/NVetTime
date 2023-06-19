using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Empleados_tareas")]
public class Empleado_tarea
{
    public int id { get; set; }

    [ForeignKey(nameof(empleadoId))]
    public int? empleadoId { get; set; }

    [ForeignKey(nameof(tareaId))]
    public int? tareaId { get; set; }
    public int? activo { get; set; }
    public Empleado? empleado { get; set; }
    public Tarea? tarea { get; set; }

    public Empleado_tarea()
    {

    }
    public Empleado_tarea(int id, int? empleadoId, int? tareaId, int? activo, Empleado? empleado, Tarea? tarea)
    {
        this.id = id;
        this.empleadoId = empleadoId;
        this.tareaId = tareaId;
        this.activo = activo;
        this.empleado = empleado;
        this.tarea = tarea;
    }
}