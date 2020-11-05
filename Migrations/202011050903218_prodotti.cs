namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prodotti : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientAddress", "ProductID", "dbo.Product");
            DropIndex("dbo.ClientAddress", new[] { "ProductID" });
            AddColumn("dbo.Product", "ClientAddress_ID", c => c.Int());
            CreateIndex("dbo.Product", "ClientAddress_ID");
            AddForeignKey("dbo.Product", "ClientAddress_ID", "dbo.ClientAddress", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "ClientAddress_ID", "dbo.ClientAddress");
            DropIndex("dbo.Product", new[] { "ClientAddress_ID" });
            DropColumn("dbo.Product", "ClientAddress_ID");
            CreateIndex("dbo.ClientAddress", "ProductID");
            AddForeignKey("dbo.ClientAddress", "ProductID", "dbo.Product", "ID", cascadeDelete: true);
        }
    }
}
