using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ValkimiaChallenge.Models
{
    public class FacturasContext : DbContext
    {
       
        public FacturasContext(DbContextOptions<FacturasContext> options): base (options) {
        
        }

        public DbSet<Ciudad> Cuidades { get; set; }
        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<Factura> Facturas { get; set; }


    }
}
