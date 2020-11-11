namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aggiuntilunghezze : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientAddress", "Address", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.ClientAddress", "Region", c => c.String(maxLength: 100));
            AlterColumn("dbo.ClientAddress", "City", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ClientAddress", "Building", c => c.String(maxLength: 50));
            AlterColumn("dbo.ClientAddress", "Apartment", c => c.String(maxLength: 50));
            AlterColumn("dbo.ClientAddress", "IndexCode", c => c.String(maxLength: 15));
            AlterColumn("dbo.Product", "SerialNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.Product", "ProductCode", c => c.String(maxLength: 100));
            AlterColumn("dbo.Product", "Model", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Product", "Serial");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Serial", c => c.String());
            AlterColumn("dbo.Product", "Model", c => c.String());
            AlterColumn("dbo.Product", "ProductCode", c => c.String());
            AlterColumn("dbo.Product", "SerialNumber", c => c.String());
            AlterColumn("dbo.ClientAddress", "IndexCode", c => c.String());
            AlterColumn("dbo.ClientAddress", "Apartment", c => c.String());
            AlterColumn("dbo.ClientAddress", "Building", c => c.String());
            AlterColumn("dbo.ClientAddress", "City", c => c.String(nullable: false));
            AlterColumn("dbo.ClientAddress", "Region", c => c.String());
            AlterColumn("dbo.ClientAddress", "Address", c => c.String(nullable: false));
        }
    }
}
