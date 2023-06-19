using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Mascotas")]
public class Mascota
{
    public int id { get; set; }

    public String? nombre { get; set; }
    public String? apellido { get; set; }
    public int? activo { get; set; }
    public DateOnly? fechaNacimiento { get; set; }
    public Decimal? peso { get; set; }
    public String? foto { get; set; }
    public String? datos_varios { get; set; }
    public String? uid { get; set; }

    public Mascota()
    {

    }
    public Mascota(int id, String? nombre, String? apellido, int? activo, DateOnly? fechaNacimiento, Decimal? peso, String? foto, String? datos_varios, String? uid)
    {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.activo = activo;
        this.fechaNacimiento = fechaNacimiento;
        this.peso = peso;
        this.foto = foto;
        this.datos_varios = datos_varios;
        this.uid = uid;
    }
}