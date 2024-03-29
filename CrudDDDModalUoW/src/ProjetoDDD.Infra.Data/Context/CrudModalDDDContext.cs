﻿using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using ProjetoDDD.Domain.Entities;
using ProjetoDDD.Infra.Data.EntityConfig;

namespace ProjetoDDD.Infra.Data.Context
{
    public class ProjetoDDDContext : DbContext
    {
        public ProjetoDDDContext()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<Conta> Contas { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Publicacao> Publicacoes { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
       


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // General Custom Context Properties
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ContaConfig());
            modelBuilder.Configurations.Add(new PerfilConfig());
            modelBuilder.Configurations.Add(new PublicacaoConfig());
            modelBuilder.Configurations.Add(new ComentarioConfig());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }

    // Classe para trocar a ConnectionString do EF em tempo de execução.
    public static class ChangeDb
    {
        public static void ChangeConnection(this ProjetoDDDContext context, string cn)
        {
            context.Database.Connection.ConnectionString = cn;
        }
    }
}