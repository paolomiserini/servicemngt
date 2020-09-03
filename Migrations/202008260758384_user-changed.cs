namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userchanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "TecnicianID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "TecnicianID");
        }
    }
}
