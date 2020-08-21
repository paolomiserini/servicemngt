namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clienttypedesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientType", "TypeCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientType", "TypeCode");
        }
    }
}
