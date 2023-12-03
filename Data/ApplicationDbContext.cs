using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Models;
using System.Reflection.Emit;

namespace HabitAqui.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckIn>()
                .HasOne(c => c.Aluguer)
                .WithOne(a => a.CheckIn)
                .HasForeignKey<CheckIn>(c => c.AluguerId);

            modelBuilder.Entity<CheckOut>()
                .HasOne(c => c.Aluguer)
                .WithOne(a => a.CheckOut)
                .HasForeignKey<CheckOut>(c => c.AluguerId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Habitacao> Habitacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Locador> Locadores { get; set; }
        public DbSet<Aluguer> Alugueres { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
    }
}