using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Tareas")]
public class Tarea
{
    public int id { get; set; }
    public String? tarea { get; set; }
    public TimeOnly? tiempo { get; set; }
    public Decimal? precio { get; set; }
    public int? activo { get; set; }

    public Tarea()
    {

    }

    public Tarea(int id, string? tarea, TimeOnly? tiempo, Decimal? precio, int? activo)
    {
        this.id = id;
        this.tarea = tarea;
        this.tiempo = tiempo;
        this.precio = precio;
        this.activo = activo;
    }

}