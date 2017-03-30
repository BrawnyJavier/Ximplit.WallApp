namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPostsandCommentstothemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ParentComment_CommentID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Comments", t => t.ParentComment_CommentID)
                .Index(t => t.ParentComment_CommentID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        Author_UserName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Users", t => t.Author_UserName)
                .Index(t => t.Author_UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Author_UserName", "dbo.Users");
            DropForeignKey("dbo.Comments", "ParentComment_CommentID", "dbo.Comments");
            DropIndex("dbo.Posts", new[] { "Author_UserName" });
            DropIndex("dbo.Comments", new[] { "ParentComment_CommentID" });
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
