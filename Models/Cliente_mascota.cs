using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NVetTime.Models;
[Table("Clientes_mascotas")]
public class Cliente_mascota
{
    public int id { get; set; }

    [ForeignKey(nameof(clienteId))]
    public int? clienteId { get; set; }

    [ForeignKey(nameof(mascotaId))]
    public int? mascotaId { get; set; }
    public int? activo { get; set; }
    public Cliente? cliente { get; set; }
    public Mascota? mascota { get; set; }

    public Cliente_mascota() { }
    public Cliente_mascota(int id, int? clienteId, int? mascotaId, int? activo, Cliente? cliente, Mascota? mascota)
    {
        this.id = id;
        this.clienteId = clienteId;
        this.mascotaId = mascotaId;
        this.activo = activo;
        this.cliente = cliente;
        this.mascota = mascota;
    }
}