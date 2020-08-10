namespace ServiceManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class utenti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        LogTableName = c.String(maxLength: 100),
                        LogIdTable = c.Int(),
                        LogUsrUpd = c.String(maxLength: 150),
                        LogDtUpd = c.DateTime(),
                        LogMessage = c.String(maxLength: 500),
                        LogType = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserType = c.String(nullable: false, maxLength: 3),
                        UserIsActive = c.Boolean(nullable: false),
                        UserLastLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.Log");
        }
    }
}
