namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientAddress",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Address = c.String(),
                        Region = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        Building = c.String(),
                        Apartment = c.String(),
                        IndexCode = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(),
                        ProductCode = c.String(),
                        Model = c.String(),
                        Serial = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanyType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Client", "ClientType", c => c.String(maxLength: 100));
            AlterColumn("dbo.Client", "Name", c => c.String());
            AlterColumn("dbo.Client", "CompanyType", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientAddress", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ClientAddress", "ClientID", "dbo.Client");
            DropIndex("dbo.ClientAddress", new[] { "ProductID" });
            DropIndex("dbo.ClientAddress", new[] { "ClientID" });
            AlterColumn("dbo.Client", "CompanyType", c => c.Int(nullable: false));
            AlterColumn("dbo.Client", "Name", c => c.String(maxLength: 100));
            DropColumn("dbo.Client", "ClientType");
            DropTable("dbo.CompanyType");
            DropTable("dbo.Product");
            DropTable("dbo.ClientAddress");
        }
    }
}
