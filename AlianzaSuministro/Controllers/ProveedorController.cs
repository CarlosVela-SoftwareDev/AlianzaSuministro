using AlianzaSuministro.Data;
using AlianzaSuministro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AlianzaSuministro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly AlianzaSuministroContext _context;

        public ProveedorController(AlianzaSuministroContext context)
        {
            _context = context;
        }

        // GET: api/Proveedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedor()
        {
            return await _context.Proveedor.ToListAsync();
        }

        // GET: api/Proveedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoProveedor>> GetProductoProveedor(int id)
        {
            var productoProveedor = await _context.ProductoProveedor
                .Include(pp => pp.Proveedor)
                .Include(pp => pp.Producto)
                .FirstOrDefaultAsync(pp => pp.Id == id);

            if (productoProveedor == null)
            {
                return NotFound();
            }

            return productoProveedor;
        }

        // GET: api/proveedor/producto/1
        [HttpGet("producto/{productoId}")]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedoresPorProducto(int productoId)
        {
            var proveedores = await _context.Proveedor
                .Include(p => p.ProductoProveedores)
                .Where(p => p.ProductoProveedores.Any(pp => pp.ProductoId == productoId))
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    ProductoProveedores = p.ProductoProveedores
                        .Where(pp => pp.ProductoId == productoId)
                        .Select(pp => new { pp.ClaveProductoProveedor, pp.Costo })
                })
                .ToListAsync();

            if (proveedores == null)
            {
                return NotFound();
            }

            return Ok(proveedores);
        }

        // PUT: api/Proveedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoProveedor(int id, [FromBody] ProductoProveedorDto productoProveedorDto)
        {
            if (id != productoProveedorDto.Id)
            {
                return BadRequest();
            }

            var productoProveedor = await _context.ProductoProveedor.FindAsync(id);
            if (productoProveedor == null)
            {
                return NotFound();
            }

            // Actualizar los campos del ProductoProveedor
            productoProveedor.ProveedorId = productoProveedorDto.ProveedorId;
            productoProveedor.ProductoId = productoProveedorDto.ProductoId;
            productoProveedor.Costo = productoProveedorDto.Costo;
            productoProveedor.ClaveProductoProveedor = productoProveedorDto.ClaveProductoProveedor;

            _context.ProductoProveedor.Update(productoProveedor);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoProveedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Proveedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductoProveedor>> PostProductoProveedor([FromBody] ProductoProveedorDto productoProveedorDto)
        {
            var productoProveedor = new ProductoProveedor
            {
                ProveedorId = productoProveedorDto.ProveedorId,
                ProductoId = productoProveedorDto.ProductoId,
                Costo = productoProveedorDto.Costo,
                ClaveProductoProveedor = productoProveedorDto.ClaveProductoProveedor
            };

            _context.ProductoProveedor.Add(productoProveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductoProveedor", new { id = productoProveedor.Id }, productoProveedor);
        }

        // DELETE: api/Proveedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task CallSaveProductoProveedor(int productoId, int proveedorId, decimal costo, string claveProductoProveedor)
        {
            // Obtener la conexión de la base de datos
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            // Crear el comando para el stored procedure
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SaveProductoProveedor";
                command.CommandType = CommandType.StoredProcedure;

                // Agregar los parámetros al comando
                command.Parameters.Add(new SqlParameter("@ProductoId", productoId));
                command.Parameters.Add(new SqlParameter("@ProveedorId", proveedorId));
                command.Parameters.Add(new SqlParameter("@Costo", costo));
                command.Parameters.Add(new SqlParameter("@ClaveProductoProveedor", claveProductoProveedor));

                // Ejecutar el comando
                await command.ExecuteNonQueryAsync();
            }

            // Cerrar la conexión
            await connection.CloseAsync();
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedor.Any(e => e.Id == id);
        }

        private bool ProductoProveedorExists(int id)
        {
            return _context.ProductoProveedor.Any(e => e.Id == id);
        }
    }
}
