using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Booking.Infra.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Email).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Senha).HasMaxLength(100).IsRequired();

            builder.Property(p => p.PrimeiroNome).HasMaxLength(20).IsRequired();

            builder.Property(p => p.UltimoNome).HasMaxLength(20).IsRequired();

            builder.Property(p => p.Cpf).HasMaxLength(20).IsRequired();
            
            builder.Property(p => p.DataNascimento).IsRequired();

            builder.Property(p => p.TipoUsuario).HasMaxLength(10).IsRequired();

        }
    }
}