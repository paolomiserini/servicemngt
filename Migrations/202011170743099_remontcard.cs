namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remontcard : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sparepart", "RemontCard_RemontCardID", "dbo.RemontCard");
            DropIndex("dbo.RemontCard", new[] { "TecnicianID" });
            DropIndex("dbo.RemontCard", new[] { "ClientID" });
            DropIndex("dbo.Sparepart", new[] { "RemontCard_RemontCardID" });
            AddColumn("dbo.RemontCard", "AddressId", c => c.Int(nullable: false));
            AddColumn("dbo.RemontCard", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.RemontCard", "RemontCardLongId", c => c.String());
            AddColumn("dbo.RemontCard", "DtClientCall", c => c.DateTime(nullable: false));
            AddColumn("dbo.RemontCard", "DtEndWork", c => c.DateTime(nullable: false));
            AddColumn("dbo.RemontCard", "DtMasterTook", c => c.DateTime(nullable: false));
            AddColumn("dbo.RemontCard", "DtLastUpdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RemontCard", "UserLastUpdate", c => c.String(nullable: false));
            AddColumn("dbo.RemontCard", "RequestState", c => c.String(nullable: false));
            AddColumn("dbo.RemontCard", "ClientProblemDescription", c => c.String(maxLength: 1000));
            AddColumn("dbo.RemontCard", "OfficeProblemDescription", c => c.String(maxLength: 1000));
            AddColumn("dbo.RemontCard", "SupervisorAdditionalNotes", c => c.String(maxLength: 1000));
            CreateIndex("dbo.RemontCard", "TecnicianId");
            CreateIndex("dbo.RemontCard", "ClientId");
            DropColumn("dbo.RemontCard", "ClientCall");
            DropColumn("dbo.RemontCard", "SolutionDate");
            DropColumn("dbo.RemontCard", "ProblemDescription");
            DropColumn("dbo.RemontCard", "AdditionalNote");
            DropColumn("dbo.RemontCard", "TypeOfWork");
            DropColumn("dbo.RemontCard", "ProductLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sparepart", "RemontCard_RemontCardID", c => c.Int());
            AddColumn("dbo.RemontCard", "ProductLocation", c => c.String(nullable: false));
            AddColumn("dbo.RemontCard", "TypeOfWork", c => c.String(nullable: false));
            AddColumn("dbo.RemontCard", "AdditionalNote", c => c.String(maxLength: 500));
            AddColumn("dbo.RemontCard", "ProblemDescription", c => c.String(maxLength: 1000));
            AddColumn("dbo.RemontCard", "SolutionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RemontCard", "ClientCall", c => c.DateTime(nullable: false));
            DropIndex("dbo.RemontCard", new[] { "ClientId" });
            DropIndex("dbo.RemontCard", new[] { "TecnicianId" });
            DropColumn("dbo.RemontCard", "SupervisorAdditionalNotes");
            DropColumn("dbo.RemontCard", "OfficeProblemDescription");
            DropColumn("dbo.RemontCard", "ClientProblemDescription");
            DropColumn("dbo.RemontCard", "RequestState");
            DropColumn("dbo.RemontCard", "UserLastUpdate");
            DropColumn("dbo.RemontCard", "DtLastUpdate");
            DropColumn("dbo.RemontCard", "DtMasterTook");
            DropColumn("dbo.RemontCard", "DtEndWork");
            DropColumn("dbo.RemontCard", "DtClientCall");
            DropColumn("dbo.RemontCard", "RemontCardLongId");
            DropColumn("dbo.RemontCard", "ProductId");
            DropColumn("dbo.RemontCard", "AddressId");
            CreateIndex("dbo.Sparepart", "RemontCard_RemontCardID");
            CreateIndex("dbo.RemontCard", "ClientID");
            CreateIndex("dbo.RemontCard", "TecnicianID");
            AddForeignKey("dbo.Sparepart", "RemontCard_RemontCardID", "dbo.RemontCard", "RemontCardID");
        }
    }
}
