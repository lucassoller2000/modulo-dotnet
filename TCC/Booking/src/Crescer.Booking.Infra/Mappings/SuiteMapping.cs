using Crescer.Booking.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Booking.Infra.Mappings
{
    public class SuiteMapping : IEntityTypeConfiguration<Suite>
    {
        public void Configure(EntityTypeBuilder<Suite> builder)
        {
            builder.ToTable("Suite");
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();

            builder.Property(p => p.Descricao).HasMaxLength(200).IsRequired();

            builder.Property(p => p.Capacidade).IsRequired();

            builder.Property(p => p.ValorDiaria).IsRequired(); 
        }
    }
}