namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tecniantable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tecnician", "Telephone", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tecnician", "Telephone", c => c.String(nullable: false));
        }
    }
}
