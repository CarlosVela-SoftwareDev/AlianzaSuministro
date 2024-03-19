using System.ComponentModel.DataAnnotations;

namespace AlianzaSuministro.Models
{
    public class TipoProducto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public ICollection<Producto>? Productos { get; set; }
    }
}
