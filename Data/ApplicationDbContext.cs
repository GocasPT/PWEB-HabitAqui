using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Models;
using System.Reflection.Emit;
using HabitAqui.ViewModels;

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

            modelBuilder.Entity<Habitacao>()
                .HasMany(h => h.Pontuacoes)
                .WithOne(p => p.Habitacao)
                .HasForeignKey(p => p.HabitacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.Pontuacoes)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Habitacao_Itens>()
                .HasKey(hi => new { hi.HabitacaoId, hi.ItemId });

            modelBuilder.Entity<Habitacao_Itens>()
                .HasOne(hi => hi.Habitacao)
                .WithMany(h => h.Itens)
                .HasForeignKey(hi => hi.HabitacaoId);

            modelBuilder.Entity<Habitacao_Itens>()
                .HasOne(hi => hi.Item)
                .WithMany(i => i.HabitacaoItens)
                .HasForeignKey(hi => hi.ItemId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Habitacao> Habitacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Locador> Locadores { get; set; }
        public DbSet<Aluguer> Alugueres { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<ApplicationUser> Administradores { get; set; }
        public DbSet<ApplicationUser> Gestores { get; set; }
        public DbSet<ApplicationUser> Funcionarios { get; set; }
        public DbSet<ApplicationUser> Clientes { get; set; }
        public DbSet<ApplicationUser> Utilizadores { get; set; }
        public DbSet<Itens> Itens { get; set; }
        public DbSet<Pontuacao> Pontuacoes { get; set; }
        public DbSet<Habitacao_Itens> Habitacoes_Itens { get; set; }
        public DbSet<Fotografia> Fotografias { get; set; }
        public DbSet<TipoLocador> TipoLocador { get; set; }
        public DbSet<Tipologia> Tipologia { get; set; }
    }
}