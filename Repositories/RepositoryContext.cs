using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.SqlServer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repositories
{
    public class RepositoryContext : DbContext
    {
        /*// DbSet'ler
        public DbSet<Kategori> Kategori { get; set; }  // İsimlendirmeyi daha tutarlı hale getirdim
        public DbSet<Makale> Makale { get; set; }
        public DbSet<Users> Users { get; set; }

        // Constructor
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
           
        }

        // Model yapılandırmalarını buradan uygulayabilirsiniz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }*/
        public DbSet<MakaleData> MakaleData { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Makale> Makale { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<UserRole> Role { get; set; }
       

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
