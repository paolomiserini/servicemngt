namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updclient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeDescription = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Client", "CompanyTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.Client", "ClientTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Client", "Name", c => c.String(maxLength: 100));
            CreateIndex("dbo.Client", "CompanyTypeID");
            CreateIndex("dbo.Client", "ClientTypeID");
            AddForeignKey("dbo.Client", "ClientTypeID", "dbo.ClientType", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Client", "CompanyTypeID", "dbo.CompanyType", "ID", cascadeDelete: true);
            DropColumn("dbo.Client", "ClientType");
            DropColumn("dbo.Client", "CompanyType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Client", "CompanyType", c => c.String());
            AddColumn("dbo.Client", "ClientType", c => c.String(maxLength: 100));
            DropForeignKey("dbo.Client", "CompanyTypeID", "dbo.CompanyType");
            DropForeignKey("dbo.Client", "ClientTypeID", "dbo.ClientType");
            DropIndex("dbo.Client", new[] { "ClientTypeID" });
            DropIndex("dbo.Client", new[] { "CompanyTypeID" });
            AlterColumn("dbo.Client", "Name", c => c.String());
            DropColumn("dbo.Client", "ClientTypeID");
            DropColumn("dbo.Client", "CompanyTypeID");
            DropTable("dbo.ClientType");
        }
    }
}
