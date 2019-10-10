using Microsoft.EntityFrameworkCore;
using PetStore.Dominio.Entidades;
using PetStore.Infra.Mapping;

namespace PetStore.Infra
{
    public class PetStoreContext : DbContext
    {
        public PetStoreContext(DbContextOptions options) : base(options){ }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<UsuarioPet> UsuarioPet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PetMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new UsuarioPetMapping());
            modelBuilder.ApplyConfiguration(new CategoriaMapping());
            modelBuilder.ApplyConfiguration(new TagMapping());
        }
    }
}