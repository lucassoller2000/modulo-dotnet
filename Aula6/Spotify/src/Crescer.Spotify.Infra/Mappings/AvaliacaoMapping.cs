using Crescer.Spotify.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Spotify.Dominio.Mappings
{
    public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable("Avaliacao");

            builder.HasKey(p => p.IdAvaliacao);

            builder.HasOne(p => p.Usuario).WithMany().IsRequired();

            builder.Property(p => p.Nota).IsRequired();

            builder.HasOne(p => p.Musica).WithMany().IsRequired();
        }
    }
}