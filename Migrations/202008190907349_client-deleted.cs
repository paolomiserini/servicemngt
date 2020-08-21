namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientdeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "IsDeleted");
        }
    }
}
