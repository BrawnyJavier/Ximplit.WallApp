namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingemailvaluetousersschema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CommentAuthor_UserName", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            CreateIndex("dbo.Comments", "CommentAuthor_UserName");
            AddForeignKey("dbo.Comments", "CommentAuthor_UserName", "dbo.Users", "UserName");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CommentAuthor_UserName", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "CommentAuthor_UserName" });
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Comments", "CommentAuthor_UserName");
        }
    }
}
