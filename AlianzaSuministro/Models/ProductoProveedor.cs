using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlianzaSuministro.Models
{
    public class ProductoProveedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required]
        public int ProveedorId { get; set; }

        [ForeignKey("ProveedorId")]
        public Proveedor? Proveedor { get; set; }

        [Required]
        public decimal Costo { get; set; }

        [Required]
        [StringLength(50)]
        public string ClaveProductoProveedor { get; set; }
    }
}
