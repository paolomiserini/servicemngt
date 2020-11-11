namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rimossostreet : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientAddress", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.ClientAddress", "City", c => c.String(nullable: false));
            DropColumn("dbo.ClientAddress", "Street");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientAddress", "Street", c => c.String());
            AlterColumn("dbo.ClientAddress", "City", c => c.String());
            AlterColumn("dbo.ClientAddress", "Address", c => c.String());
        }
    }
}
