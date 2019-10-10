using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Booking.Infra.Mappings
{
    public class OpcionalMapping : IEntityTypeConfiguration<Opcional>
    {
        public void Configure(EntityTypeBuilder<Opcional> builder)
        {
            builder.ToTable("Opcional");
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();

            builder.Property(p => p.Descricao).HasMaxLength(200).IsRequired();

            builder.Property(p => p.Valor).IsRequired();
        }
    }
}