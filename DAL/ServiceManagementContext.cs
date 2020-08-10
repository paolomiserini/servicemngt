using ServiceManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ServiceManagement.DAL
{
    public class ServiceManagementContext : DbContext
    {
        public ServiceManagementContext() : base("ServiceManagementContext")
        {
        }

        public DbSet<Sparepart> Spareparts { get; set; }
        public DbSet<Tecnician> Tecnicians { get; set; }
        public DbSet<RemontCard> RemontCards { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<ClientAddress> ClientAddresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}