namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStoredemailsobj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WallAppStoredEmails",
                c => new
                    {
                        EmailId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Body = c.String(nullable: false),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.EmailId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WallAppStoredEmails");
        }
    }
}
