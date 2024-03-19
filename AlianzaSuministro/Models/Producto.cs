using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AlianzaSuministro.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Clave { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int TipoProductoId { get; set; }

        [BindNever]
        [ForeignKey("TipoProductoId")]
        public TipoProducto? TipoProducto { get; set; }

        [Required]
        public bool EsActivo { get; set; }

        public decimal? Precio { get; set; }
        
        [BindNever]
        public ICollection<ProductoProveedor>? ProductoProveedores { get; set; }
    }
}
