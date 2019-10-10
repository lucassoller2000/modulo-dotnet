using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Dominio.Entidades;

namespace PetStore.Infra.Mapping
{
    public class UsuarioPetMapping : IEntityTypeConfiguration<UsuarioPet>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UsuarioPet> builder)
        {
            builder.ToTable("UsuarioPet");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Pet).WithMany();

            builder.HasOne(p => p.Usuario).WithMany();
        }
    }
}