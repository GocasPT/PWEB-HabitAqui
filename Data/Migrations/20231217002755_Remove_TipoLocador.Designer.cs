﻿// <auto-generated />
using System;
using HabitAqui.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HabitAqui.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231217002755_Remove_TipoLocador")]
    partial class Remove_TipoLocador
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HabitAqui.Models.Aluguer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CheckInId")
                        .HasColumnType("int");

                    b.Property<int?>("CheckOutId")
                        .HasColumnType("int");

                    b.Property<string>("ClienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Confirmado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataDeEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeSaida")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HabitacaoId")
                        .HasColumnType("int");

                    b.Property<int?>("LocadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("HabitacaoId");

                    b.HasIndex("LocadorId");

                    b.ToTable("Alugueres");
                });

            modelBuilder.Entity("HabitAqui.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckIn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AluguerId")
                        .HasColumnType("int");

                    b.Property<string>("Danos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataCheckIn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FuncionarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Observacoes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AluguerId")
                        .IsUnique()
                        .HasFilter("[AluguerId] IS NOT NULL");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("CheckIns");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckInItem", b =>
                {
                    b.Property<int?>("CheckInId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("CheckInId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("CheckInItems");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckOut", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AluguerId")
                        .HasColumnType("int");

                    b.Property<string>("Danos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataCheckOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("FuncionarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Observacoes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AluguerId")
                        .IsUnique()
                        .HasFilter("[AluguerId] IS NOT NULL");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("CheckOuts");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckOutItem", b =>
                {
                    b.Property<int?>("CheckOutId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("CheckOutId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("CheckOutItems");
                });

            modelBuilder.Entity("HabitAqui.Models.Fotografia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CheckOutId")
                        .HasColumnType("int");

                    b.Property<string>("Extensao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HabitacaoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CheckOutId");

                    b.HasIndex("HabitacaoId");

                    b.ToTable("Fotografias");
                });

            modelBuilder.Entity("HabitAqui.Models.Habitacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Concelho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustoPorNoite")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<string>("Distrito")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<int?>("LocadorId")
                        .HasColumnType("int");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumPessoas")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("NumWC")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Pais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TipologiaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("LocadorId");

                    b.HasIndex("TipologiaId");

                    b.ToTable("Habitacoes");
                });

            modelBuilder.Entity("HabitAqui.Models.Habitacao_Itens", b =>
                {
                    b.Property<int>("HabitacaoId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("HabitacaoId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("Habitacoes_Itens");
                });

            modelBuilder.Entity("HabitAqui.Models.Itens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Categoria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Itens");
                });

            modelBuilder.Entity("HabitAqui.Models.Locador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Contacto")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsEmpresa")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumFuncionarios")
                        .HasColumnType("int");

                    b.Property<int?>("NumGestores")
                        .HasColumnType("int");

                    b.Property<int?>("NumHabitacoes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Locadores");
                });

            modelBuilder.Entity("HabitAqui.Models.Pontuacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HabitacaoId")
                        .HasColumnType("int");

                    b.Property<double>("PontuacaoEspaco")
                        .HasColumnType("float");

                    b.Property<double>("PontuacaoLimpeza")
                        .HasColumnType("float");

                    b.Property<double>("PontuacaoLocalizacao")
                        .HasColumnType("float");

                    b.Property<double>("PontuacaoQualidadePreco")
                        .HasColumnType("float");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("HabitacaoId");

                    b.ToTable("Pontuacoes");
                });

            modelBuilder.Entity("HabitAqui.Models.Tipologia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tipologia");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HabitAqui.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("CargoComLocador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LocadorId")
                        .HasColumnType("int");

                    b.Property<string>("PrimeiroNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UltimoNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("LocadorId");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("HabitAqui.Models.Aluguer", b =>
                {
                    b.HasOne("HabitAqui.Models.ApplicationUser", "Cliente")
                        .WithMany("Alugueres")
                        .HasForeignKey("ClienteId");

                    b.HasOne("HabitAqui.Models.Habitacao", "Habitacao")
                        .WithMany("Alugueres")
                        .HasForeignKey("HabitacaoId");

                    b.HasOne("HabitAqui.Models.Locador", "Locador")
                        .WithMany()
                        .HasForeignKey("LocadorId");

                    b.Navigation("Cliente");

                    b.Navigation("Habitacao");

                    b.Navigation("Locador");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckIn", b =>
                {
                    b.HasOne("HabitAqui.Models.Aluguer", "Aluguer")
                        .WithOne("CheckIn")
                        .HasForeignKey("HabitAqui.Models.CheckIn", "AluguerId");

                    b.HasOne("HabitAqui.Models.ApplicationUser", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioId");

                    b.Navigation("Aluguer");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckInItem", b =>
                {
                    b.HasOne("HabitAqui.Models.CheckIn", "CheckIn")
                        .WithMany("CheckInItems")
                        .HasForeignKey("CheckInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HabitAqui.Models.Itens", "Items")
                        .WithMany("CheckInItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckIn");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckOut", b =>
                {
                    b.HasOne("HabitAqui.Models.Aluguer", "Aluguer")
                        .WithOne("CheckOut")
                        .HasForeignKey("HabitAqui.Models.CheckOut", "AluguerId");

                    b.HasOne("HabitAqui.Models.ApplicationUser", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioId");

                    b.Navigation("Aluguer");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckOutItem", b =>
                {
                    b.HasOne("HabitAqui.Models.CheckOut", "CheckOut")
                        .WithMany("CheckOutItems")
                        .HasForeignKey("CheckOutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HabitAqui.Models.Itens", "Items")
                        .WithMany("CheckOutItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckOut");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("HabitAqui.Models.Fotografia", b =>
                {
                    b.HasOne("HabitAqui.Models.CheckOut", "CheckOut")
                        .WithMany("Fotografias")
                        .HasForeignKey("CheckOutId");

                    b.HasOne("HabitAqui.Models.Habitacao", "Habitacao")
                        .WithMany("Fotografias")
                        .HasForeignKey("HabitacaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("CheckOut");

                    b.Navigation("Habitacao");
                });

            modelBuilder.Entity("HabitAqui.Models.Habitacao", b =>
                {
                    b.HasOne("HabitAqui.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HabitAqui.Models.Locador", "Locador")
                        .WithMany("Habitacoes")
                        .HasForeignKey("LocadorId");

                    b.HasOne("HabitAqui.Models.Tipologia", "Tipologia")
                        .WithMany()
                        .HasForeignKey("TipologiaId");

                    b.Navigation("Categoria");

                    b.Navigation("Locador");

                    b.Navigation("Tipologia");
                });

            modelBuilder.Entity("HabitAqui.Models.Habitacao_Itens", b =>
                {
                    b.HasOne("HabitAqui.Models.Habitacao", "Habitacao")
                        .WithMany("Itens")
                        .HasForeignKey("HabitacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HabitAqui.Models.Itens", "Item")
                        .WithMany("HabitacaoItens")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habitacao");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("HabitAqui.Models.Pontuacao", b =>
                {
                    b.HasOne("HabitAqui.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Pontuacoes")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HabitAqui.Models.Habitacao", "Habitacao")
                        .WithMany("Pontuacoes")
                        .HasForeignKey("HabitacaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ApplicationUser");

                    b.Navigation("Habitacao");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HabitAqui.Models.ApplicationUser", b =>
                {
                    b.HasOne("HabitAqui.Models.Locador", "Locador")
                        .WithMany("GestoresFuncionarios")
                        .HasForeignKey("LocadorId");

                    b.Navigation("Locador");
                });

            modelBuilder.Entity("HabitAqui.Models.Aluguer", b =>
                {
                    b.Navigation("CheckIn");

                    b.Navigation("CheckOut");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckIn", b =>
                {
                    b.Navigation("CheckInItems");
                });

            modelBuilder.Entity("HabitAqui.Models.CheckOut", b =>
                {
                    b.Navigation("CheckOutItems");

                    b.Navigation("Fotografias");
                });

            modelBuilder.Entity("HabitAqui.Models.Habitacao", b =>
                {
                    b.Navigation("Alugueres");

                    b.Navigation("Fotografias");

                    b.Navigation("Itens");

                    b.Navigation("Pontuacoes");
                });

            modelBuilder.Entity("HabitAqui.Models.Itens", b =>
                {
                    b.Navigation("CheckInItems");

                    b.Navigation("CheckOutItems");

                    b.Navigation("HabitacaoItens");
                });

            modelBuilder.Entity("HabitAqui.Models.Locador", b =>
                {
                    b.Navigation("GestoresFuncionarios");

                    b.Navigation("Habitacoes");
                });

            modelBuilder.Entity("HabitAqui.Models.ApplicationUser", b =>
                {
                    b.Navigation("Alugueres");

                    b.Navigation("Pontuacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
