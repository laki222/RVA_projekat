using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Common.Model;

namespace Service
{
   public class RacunDbContext : DbContext
    {

    public DbSet<Racun> Racuni { get; set; }

    public DbSet<Admin> Admini { get; set; }

    public DbSet<RegistrovaniKupac> RegistrovaniKupci { get; set; }

    public DbSet<Proizvod> Proizvodi { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<RacunDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }



    }
}
