using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlianzaSuministro.Models;

namespace AlianzaSuministro.Data
{
    public class AlianzaSuministroContext : DbContext
    {
        public AlianzaSuministroContext (DbContextOptions<AlianzaSuministroContext> options)
            : base(options)
        {
        }

        public DbSet<AlianzaSuministro.Models.Producto> Producto { get; set; } = default!;
        public DbSet<AlianzaSuministro.Models.Proveedor> Proveedor { get; set; } = default!;
        public DbSet<AlianzaSuministro.Models.ProductoProveedor> ProductoProveedor { get; set; } = default!;
    }
}
