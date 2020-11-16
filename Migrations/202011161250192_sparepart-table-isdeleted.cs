namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spareparttableisdeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sparepart", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sparepart", "isDeleted");
        }
    }
}
