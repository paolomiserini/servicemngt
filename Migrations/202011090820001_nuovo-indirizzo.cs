namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuovoindirizzo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "ClientAddress_ID", "dbo.ClientAddress");
            DropIndex("dbo.Product", new[] { "ClientAddress_ID" });
            RenameColumn(table: "dbo.Product", name: "ClientAddress_ID", newName: "ClientAddressID");
            AlterColumn("dbo.Product", "ClientAddressID", c => c.Int(nullable: false));
            CreateIndex("dbo.Product", "ClientAddressID");
            AddForeignKey("dbo.Product", "ClientAddressID", "dbo.ClientAddress", "ID", cascadeDelete: true);
            DropColumn("dbo.ClientAddress", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientAddress", "ProductID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Product", "ClientAddressID", "dbo.ClientAddress");
            DropIndex("dbo.Product", new[] { "ClientAddressID" });
            AlterColumn("dbo.Product", "ClientAddressID", c => c.Int());
            RenameColumn(table: "dbo.Product", name: "ClientAddressID", newName: "ClientAddress_ID");
            CreateIndex("dbo.Product", "ClientAddress_ID");
            AddForeignKey("dbo.Product", "ClientAddress_ID", "dbo.ClientAddress", "ID");
        }
    }
}
