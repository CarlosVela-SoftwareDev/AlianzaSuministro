namespace AlianzaSuministro.Models
{
    public class ProveedorDto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ProductoId { get; set; }
        public int ProveedorId { get; set; }
        public decimal Costo { get; set; }
        public string ClaveProductoProveedor { get; set; }
    }
}
