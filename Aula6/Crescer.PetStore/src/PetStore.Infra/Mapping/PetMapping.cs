using Microsoft.EntityFrameworkCore;
using PetStore.Dominio.Entidades;

namespace PetStore.Infra.Mapping
{
    public class PetMapping : IEntityTypeConfiguration<Pet>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("Pet");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasMaxLength(20);

            builder.Property(p => p.Status);

            builder.HasMany(p => p.Tags).WithOne();

            builder.HasOne(p => p.Categoria).WithMany();
        }
    }
}