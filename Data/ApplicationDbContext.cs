using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HabitAqui.Models;
using System.Reflection.Emit;

namespace HabitAqui.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Habitacao> Habitacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}