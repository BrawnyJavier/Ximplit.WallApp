namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Req_Comments_Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "CommentAuthor_UserName", "dbo.Users");
            DropForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "CommentAuthor_UserName" });
            DropIndex("dbo.Comments", new[] { "Post_PostId" });
            AlterColumn("dbo.Comments", "CommentAuthor_UserName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "Post_PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "CommentAuthor_UserName");
            CreateIndex("dbo.Comments", "Post_PostId");
            AddForeignKey("dbo.Comments", "CommentAuthor_UserName", "dbo.Users", "UserName", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts", "PostId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "CommentAuthor_UserName", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "Post_PostId" });
            DropIndex("dbo.Comments", new[] { "CommentAuthor_UserName" });
            AlterColumn("dbo.Comments", "Post_PostId", c => c.Int());
            AlterColumn("dbo.Comments", "CommentAuthor_UserName", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "Post_PostId");
            CreateIndex("dbo.Comments", "CommentAuthor_UserName");
            AddForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts", "PostId");
            AddForeignKey("dbo.Comments", "CommentAuthor_UserName", "dbo.Users", "UserName");
        }
    }
}
