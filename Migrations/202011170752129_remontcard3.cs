namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remontcard3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RemontCard", "Tecnicianid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.RemontCard", "Tecnician", c => c.Int(nullable: false));
            DropForeignKey("dbo.RemontCard", "Tecnicianid", "dbo.Tecnician");
            DropIndex("dbo.RemontCard", new[] { "Tecnicianid" });
            AlterColumn("dbo.RemontCard", "Tecnicianid", c => c.Int());
            RenameColumn(table: "dbo.RemontCard", name: "Tecnicianid", newName: "Tecnician_ID");
            CreateIndex("dbo.RemontCard", "Tecnician_ID");
            AddForeignKey("dbo.RemontCard", "Tecnician_ID", "dbo.Tecnician", "ID");
        }
    }
}
