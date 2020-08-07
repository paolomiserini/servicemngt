namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tecnician", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Tecnician", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Tecnician", "Patronimic", c => c.String(nullable: false));
            AlterColumn("dbo.Tecnician", "Telephone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tecnician", "Telephone", c => c.String());
            AlterColumn("dbo.Tecnician", "Patronimic", c => c.String());
            AlterColumn("dbo.Tecnician", "Surname", c => c.String());
            AlterColumn("dbo.Tecnician", "Name", c => c.String());
        }
    }
}
