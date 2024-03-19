namespace AlianzaSuministro.Models
{
    public class ProductoProveedorDto
    {
        public int? Id { get; set; }
        public int ProveedorId { get; set; }
        public int ProductoId { get; set; }
        public decimal Costo { get; set; }
        public string ClaveProductoProveedor { get; set; }
    }
}
