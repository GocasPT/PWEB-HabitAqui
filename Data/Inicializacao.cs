using HabitAqui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HabitAqui.Data
{
    public enum Roles
    {
        Administrador,
        Gestor,
        Funcionario,
        Cliente
    }

    public static class Inicializacao
    {
        public static async Task CriaDadosIniciais(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Adicionar default Roles 
            await roleManager.CreateAsync(new IdentityRole(Roles.Administrador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Gestor.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Funcionario.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cliente.ToString()));

            //Adicionar Default User - Admin
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@localhost.com",
                Email = "admin@localhost.com",
                PrimeiroNome = "Administrador",
                UltimoNome = "Local",
                Ativo = true,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Is3C..00");
                await userManager.AddToRoleAsync(defaultUser, Roles.Administrador.ToString());
            }

            //APAGAR ISTO DEPOIS
            var defaultUser1 = new ApplicationUser
            {
                UserName = "gestor@localhost.com",
                Email = "gestor@localhost.com",
                PrimeiroNome = "Gestor",
                UltimoNome = "Banana",
                LocadorId = 8,
                Ativo = true,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var user1 = await userManager.FindByEmailAsync(defaultUser1.Email);
            if (user1 == null)
            {
                await userManager.CreateAsync(defaultUser1, "Is3C..00");
                await userManager.AddToRoleAsync(defaultUser1, Roles.Gestor.ToString());
            }

            //APAGAR ISTO DEPOIS
            var defaultUser2 = new ApplicationUser
            {
                UserName = "funcionario@localhost.com",
                Email = "funcionario@localhost.com",
                PrimeiroNome = "Funcionario",
                UltimoNome = "Banana",
                LocadorId = 8,
                Ativo = true,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var user2 = await userManager.FindByEmailAsync(defaultUser2.Email);
            if (user2 == null)
            {
                await userManager.CreateAsync(defaultUser2, "Is3C..00");
                await userManager.AddToRoleAsync(defaultUser2, Roles.Funcionario.ToString());
            }
        }
    }
}
