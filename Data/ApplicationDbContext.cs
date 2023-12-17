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

            modelBuilder.Entity<CheckInItem>()
            .HasKey(ci => new { ci.CheckInId, ci.ItemId });

            modelBuilder.Entity<CheckInItem>()
                .HasOne(ci => ci.CheckIn)
                .WithMany(c => c.CheckInItems)
                .HasForeignKey(ci => ci.CheckInId);

            modelBuilder.Entity<CheckInItem>()
                .HasOne(ci => ci.Items)
                .WithMany(i => i.CheckInItems)
                .HasForeignKey(ci => ci.ItemId);

            modelBuilder.Entity<CheckOutItem>()
            .HasKey(ci => new { ci.CheckOutId, ci.ItemId });

            modelBuilder.Entity<CheckOutItem>()
                .HasOne(ci => ci.CheckOut)
                .WithMany(c => c.CheckOutItems)
                .HasForeignKey(ci => ci.CheckOutId);

            modelBuilder.Entity<CheckOutItem>()
                .HasOne(ci => ci.Items)
                .WithMany(i => i.CheckOutItems)
                .HasForeignKey(ci => ci.ItemId);

            modelBuilder.Entity<Fotografia>()
                .HasOne(f => f.CheckOut)
                .WithMany(c => c.Fotografias)
                .HasForeignKey(f => f.CheckOutId);

            modelBuilder.Entity<Fotografia>()
                .HasOne(f => f.Habitacao)
                .WithMany(h => h.Fotografias)
                .HasForeignKey(f => f.HabitacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Aluguer>()
                .HasOne(a => a.Pontuacao)
                .WithOne(p => p.Aluguer)
                .HasForeignKey<Pontuacao>(p => p.AluguerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pontuacao>()
                .HasOne(p => p.Aluguer)
                .WithOne(a => a.Pontuacao)
                .HasForeignKey<Aluguer>(a => a.PontuacaoId);

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
        public DbSet<Tipologia> Tipologia { get; set; }
        public DbSet<CheckInItem> CheckInItems { get; set; }
        public DbSet<CheckOutItem> CheckOutItems { get; set; }
    }
}