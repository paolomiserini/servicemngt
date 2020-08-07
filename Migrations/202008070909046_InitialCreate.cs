namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RemontCard",
                c => new
                    {
                        RemontCardID = c.Int(nullable: false, identity: true),
                        TecnicianID = c.Int(nullable: false),
                        ClientCall = c.DateTime(nullable: false),
                        SolutionDate = c.DateTime(nullable: false),
                        ProblemDescription = c.String(maxLength: 1000),
                        AdditionalNote = c.String(maxLength: 500),
                        NoNeedSpareParts = c.Boolean(nullable: false),
                        Warranty = c.Boolean(nullable: false),
                        TypeOfWork = c.String(nullable: false),
                        ProductLocation = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        AdditionalAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.RemontCardID)
                .ForeignKey("dbo.Tecnician", t => t.TecnicianID, cascadeDelete: true)
                .Index(t => t.TecnicianID);
            
            CreateTable(
                "dbo.Sparepart",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PartCode = c.String(),
                        PartDescription = c.String(maxLength: 500),
                        PartPrice = c.Double(nullable: false),
                        RemontCard_RemontCardID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RemontCard", t => t.RemontCard_RemontCardID)
                .Index(t => t.RemontCard_RemontCardID);
            
            CreateTable(
                "dbo.Tecnician",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Patronimic = c.String(),
                        Telephone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RemontCard", "TecnicianID", "dbo.Tecnician");
            DropForeignKey("dbo.Sparepart", "RemontCard_RemontCardID", "dbo.RemontCard");
            DropIndex("dbo.Sparepart", new[] { "RemontCard_RemontCardID" });
            DropIndex("dbo.RemontCard", new[] { "TecnicianID" });
            DropTable("dbo.Tecnician");
            DropTable("dbo.Sparepart");
            DropTable("dbo.RemontCard");
        }
    }
}
