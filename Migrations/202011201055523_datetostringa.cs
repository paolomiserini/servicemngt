namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetostringa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RemontCard", "DtClientCall", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.RemontCard", "DtEndWork", c => c.String(maxLength: 10));
            AlterColumn("dbo.RemontCard", "DtMasterTook", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RemontCard", "DtMasterTook", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RemontCard", "DtEndWork", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RemontCard", "DtClientCall", c => c.DateTime(nullable: false));
        }
    }
}
