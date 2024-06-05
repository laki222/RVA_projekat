namespace Service.Migrations
{
    using Common.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Xml.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<Service.BillDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Service.BillDbContext context)
        {
            // Authors
            IList<Product> defaultProducts = new List<Product>();
            Product bananica = new Product() { Name = "Krem bananica", Price = 25, Manufacturer = "Stark", IsDeleted = false };
            Product cips = new Product() { Name = "Cips", Price = 70, Manufacturer = "Marbo", IsDeleted = false };
            
            defaultProducts.Add(bananica);
            defaultProducts.Add(cips) ;

            foreach (var a in defaultProducts)
                context.Products.AddOrUpdate(a);

            // Members
            context.RegisteredCustomers.AddOrUpdate(new RegisteredCustomer() { FirstName = "Lazar", LastName = "Brankovic", Username = "laki", Password = "1234" });

            // Admins
            context.Admins.AddOrUpdate(new Admin() { FirstName = "Lazar", LastName = "Brankovic", Username = "admin", Password = "admin" });

            BillService billService= new BillService();
            Bill bill= billService.CreateBill();


            IList<BillProducts> defaultBill = new List<BillProducts>();


            defaultBill.Add(new BillProducts() { BillId = bill.BillID, ProductName= bananica.Name});
            defaultBill.Add(new BillProducts() { BillId = bill.BillID, ProductName = cips.Name });




            foreach (var b in defaultBill)
            {
                context.BillProducts.AddOrUpdate(b);
                bill.Price += b.Price;
            }
        }
    }
}
