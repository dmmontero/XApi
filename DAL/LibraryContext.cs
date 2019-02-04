using DAL.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    class LibraryContext : DbContext
    {
        public DbSet<Book> Book { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"server=localhost;port=3306;user=root;password=Cali2017*;database=LibraryDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Fluent API configuration
            ConfigureModels(modelBuilder);
        }

        /// <summary>
        /// Fluent API configuration
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN);
                entity.Property(e => e.Title).IsRequired();
                entity.HasOne(d => d.Publisher)
                  .WithMany(p => p.Books);
            });
        }
    }
}
