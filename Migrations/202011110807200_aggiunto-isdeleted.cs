namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aggiuntoisdeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientAddress", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "idDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "idDeleted");
            DropColumn("dbo.ClientAddress", "isDeleted");
        }
    }
}
