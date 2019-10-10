using Crescer.Spotify.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crescer.Spotify.Dominio.Mappings
{
    public class MusicaMapping : IEntityTypeConfiguration<Musica>
    {
        public void Configure(EntityTypeBuilder<Musica> builder)
        {
            builder.ToTable("Musica");

            builder.HasKey(p => p.IdMusica);

            builder.Property(p => p.Nome).HasMaxLength(20).IsRequired();

            builder.Property(p => p.Duracao).IsRequired();

            builder.HasOne(p => p.Album).WithMany().IsRequired();
        }
    }
}