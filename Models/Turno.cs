using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Turnos")]
public class Turno
{

    public int id { get; set; }
    public TimeOnly? monday_ini { get; set; }
    public TimeOnly? monday_fin { get; set; }
    public TimeOnly? tuesday_ini { get; set; }
    public TimeOnly? tuesday_fin { get; set; }
    public TimeOnly? wednesday_ini { get; set; }
    public TimeOnly? wednesday_fin { get; set; }
    public TimeOnly? thursday_ini { get; set; }
    public TimeOnly? thursday_fin { get; set; }
    public TimeOnly? friday_ini { get; set; }
    public TimeOnly? friday_fin { get; set; }
    public TimeOnly? saturday_ini { get; set; }
    public TimeOnly? saturday_fin { get; set; }
    public TimeOnly? sunday_ini { get; set; }
    public TimeOnly? sunday_fin { get; set; }
    [ForeignKey(nameof(empleadoId))]
    public int? empleadoId { get; set; }
    public int? activo { get; set; }
    public Empleado? empleado { get; set; }
    public Turno()
    {

    }

    public Turno(int id, TimeOnly monday_ini, TimeOnly monday_fin, TimeOnly tuesday_ini, TimeOnly tuesday_fin,
    TimeOnly wednesday_ini, TimeOnly wednesday_fin, TimeOnly thursday_ini, TimeOnly thursday_fin, TimeOnly friday_ini,
    TimeOnly friday_fin, TimeOnly saturday_ini, TimeOnly saturday_fin, TimeOnly sunday_ini, TimeOnly sunday_fin,
    int? empleadoId, int? activo, Empleado? empleado)
    {
        this.monday_ini = monday_ini;
        this.monday_fin = monday_fin;
        this.tuesday_ini = tuesday_ini;
        this.tuesday_fin = tuesday_fin;
        this.wednesday_ini = wednesday_ini;
        this.wednesday_fin = wednesday_fin;
        this.thursday_ini = thursday_ini;
        this.thursday_fin = thursday_fin;
        this.friday_ini = friday_ini;
        this.friday_fin = friday_fin;
        this.saturday_ini = saturday_ini;
        this.saturday_fin = saturday_fin;
        this.sunday_ini = sunday_ini;
        this.sunday_fin = sunday_fin;
        this.id = id;
        this.empleadoId = empleadoId;
        this.activo = activo;
        this.empleado = empleado;
    }

}