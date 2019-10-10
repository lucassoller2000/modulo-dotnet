using Microsoft.EntityFrameworkCore;
using PetStore.Dominio.Entidades;

namespace PetStore.Infra.Mapping
{
    public class TagMapping : IEntityTypeConfiguration<Tag>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao).HasMaxLength(20);
        }
    }
}