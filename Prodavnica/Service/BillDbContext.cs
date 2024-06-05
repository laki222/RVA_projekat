using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Common.Model;

namespace Service
{
   public class BillDbContext : DbContext
    {
        private static readonly string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True;";

        // Lazy initialization for the singleton instance
        private static readonly Lazy<BillDbContext> instance = new Lazy<BillDbContext>(() => new BillDbContext());

        // Private constructor to prevent direct instantiation
        public BillDbContext() : base(connectionString)
        {
            //log.Info("MyDbContext instance created.");
        }

        // Public static property to access the singleton instance
        public static BillDbContext Instance => instance.Value;
        public DbSet<Bill> Bills { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<RegisteredCustomer> RegisteredCustomers { get; set; }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<BillProducts> BillProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BillDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }



    }
}
