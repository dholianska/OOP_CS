namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-M0HG37P\\SQLEXPRESS;Database=SalesDB;Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .Property(s => s.Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>() // second part
                .Property(p => p.Description)
                .HasMaxLength(250)       
                .HasDefaultValue("No description");
        }
    }
}