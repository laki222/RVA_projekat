namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.BillProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        Product_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BillID)
                .ForeignKey("dbo.Products", t => t.Product_Name)
                .Index(t => t.Product_Name);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Manufacturer = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.RegisteredCustomers",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Discount = c.Single(nullable: false),
                        IsDeleted = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "Product_Name", "dbo.Products");
            DropForeignKey("dbo.BillProducts", "BillId", "dbo.Bills");
            DropIndex("dbo.Bills", new[] { "Product_Name" });
            DropIndex("dbo.BillProducts", new[] { "BillId" });
            DropTable("dbo.RegisteredCustomers");
            DropTable("dbo.Products");
            DropTable("dbo.Bills");
            DropTable("dbo.BillProducts");
            DropTable("dbo.Admins");
        }
    }
}
