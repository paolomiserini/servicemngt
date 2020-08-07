namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class client : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Surname = c.String(maxLength: 100),
                        Patronimic = c.String(maxLength: 100),
                        Telephone = c.String(nullable: false, maxLength: 15),
                        CompanyType = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 150),
                        ContactPerson = c.String(),
                        ExtraInfo = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RemontCard", "ClientID", c => c.Int(nullable: false));
            CreateIndex("dbo.RemontCard", "ClientID");
            AddForeignKey("dbo.RemontCard", "ClientID", "dbo.Client", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RemontCard", "ClientID", "dbo.Client");
            DropIndex("dbo.RemontCard", new[] { "ClientID" });
            DropColumn("dbo.RemontCard", "ClientID");
            DropTable("dbo.Client");
        }
    }
}
