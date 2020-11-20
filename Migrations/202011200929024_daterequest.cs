namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class daterequest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RemontCard", "RemontCardLongId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RemontCard", "RemontCardLongId", c => c.String());
        }
    }
}
