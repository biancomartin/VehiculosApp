using Microsoft.EntityFrameworkCore;
using Vehiculos.Models;

namespace Vehiculos.AccesoDatos
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehiculo>().ToTable("Vehiculo");
        }
    }
}
