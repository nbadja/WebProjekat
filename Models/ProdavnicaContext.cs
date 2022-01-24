using Microsoft.EntityFrameworkCore;


namespace Models
{
    public class ProdavnicaContext: DbContext
    {
        public DbSet<Predmet> Predmeti {get;set;}

        public DbSet<Drzava> Drzave {get;set;}

        public DbSet<Grad> Gradovi {get;set;}

        public DbSet<Prodavnica> Prodavnice {get;set;}

        public DbSet<Storage> Storages {get;set;}

        public ProdavnicaContext(DbContextOptions Options) : base(Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            base.OnModelCreating(Builder);

            Builder.Entity<Predmet>()
            .HasMany(n => n.Storages);
        }

    }
}