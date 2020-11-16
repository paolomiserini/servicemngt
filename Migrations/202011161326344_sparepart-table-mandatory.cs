namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spareparttablemandatory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sparepart", "PartDescription", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sparepart", "PartDescription", c => c.String(maxLength: 500));
        }
    }
}
