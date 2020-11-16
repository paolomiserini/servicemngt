namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spareparttable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sparepart", "PartCode", c => c.String(maxLength: 150));
            AlterColumn("dbo.Sparepart", "PartPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sparepart", "PartPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Sparepart", "PartCode", c => c.String());
        }
    }
}
