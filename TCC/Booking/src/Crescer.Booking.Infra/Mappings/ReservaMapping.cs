using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Booking.Infra.Mappings
{
    public class ReservaMapping : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reserva");
            
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Usuario).WithMany().IsRequired();

            builder.HasOne(p => p.Suite).WithMany().IsRequired();

            builder.Property(p => p.NumeroPessoas).IsRequired();

            builder.Property(p => p.DataInicio).IsRequired();

            builder.Property(p => p.DataFim).IsRequired();

            builder.Property(p => p.subTotal);

            builder.Property(p => p.valorTotal);

            builder.Ignore(p => p.Opcionais);
        }
    }
}