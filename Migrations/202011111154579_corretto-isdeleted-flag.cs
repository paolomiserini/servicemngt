namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correttoisdeletedflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "isDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Product", "idDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "idDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Product", "isDeleted");
        }
    }
}
