namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tecniciandel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tecnician", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tecnician", "IsDeleted");
        }
    }
}
