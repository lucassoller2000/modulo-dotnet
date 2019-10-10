using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Booking.Infra.Mappings
{
    public class ReservaOpcionalMapping : IEntityTypeConfiguration<ReservaOpcional>
    {
        public void Configure(EntityTypeBuilder<ReservaOpcional> builder)
        {
            builder.ToTable("ReservaOpcional");
            
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Reserva).WithMany().IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Opcional).WithMany().IsRequired();
        }
    }
}