using Crescer.Booking.Dominio.Entidades;
using Crescer.Booking.Infra.Mappings;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Crescer.Booking.Infra
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions options) : base(options){ }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Suite> Suites { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Opcional> Opcionais { get; set; }

        public DbSet<ReservaOpcional> ReservasOpcionais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new SuiteMapping());
            modelBuilder.ApplyConfiguration(new ReservaMapping());
            modelBuilder.ApplyConfiguration(new OpcionalMapping());
            modelBuilder.ApplyConfiguration(new ReservaOpcionalMapping());
        }
    }
}