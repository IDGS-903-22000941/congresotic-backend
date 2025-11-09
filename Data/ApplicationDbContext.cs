using CongresoTicAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CongresoTicAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Participante> Participantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Participante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellidos).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Twitter).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Ocupacion).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Avatar).HasMaxLength(250);
                entity.Property(e => e.FechaRegistro)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

            });
        }
    }
}