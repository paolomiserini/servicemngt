namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remontcard2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RemontCard", "TecnicianId", "dbo.Tecnician");
            DropIndex("dbo.RemontCard", new[] { "TecnicianId" });
            RenameColumn(table: "dbo.RemontCard", name: "TecnicianId", newName: "Tecnician_ID");
            AddColumn("dbo.RemontCard", "Tecnician", c => c.Int(nullable: false));
            AlterColumn("dbo.RemontCard", "Tecnician_ID", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RemontCard", "Tecnician_ID", "dbo.Tecnician");
            DropIndex("dbo.RemontCard", new[] { "Tecnician_ID" });
            AlterColumn("dbo.RemontCard", "Tecnician_ID", c => c.Int(nullable: false));
            DropColumn("dbo.RemontCard", "Tecnician");
            RenameColumn(table: "dbo.RemontCard", name: "Tecnician_ID", newName: "TecnicianId");
            CreateIndex("dbo.RemontCard", "TecnicianId");
            AddForeignKey("dbo.RemontCard", "TecnicianId", "dbo.Tecnician", "ID", cascadeDelete: true);
        }
    }
}
