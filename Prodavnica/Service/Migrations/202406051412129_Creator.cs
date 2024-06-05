namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "Creator", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bills", "Creator");
        }
    }
}
