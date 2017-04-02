namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingusernamedatatype : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CommentsLikes");
            DropPrimaryKey("dbo.PostsLikes");
            AddColumn("dbo.CommentsLikes", "UserName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PostsLikes", "UserName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CommentsLikes", new[] { "UserName", "LikedCommentID" });
            AddPrimaryKey("dbo.PostsLikes", new[] { "UserName", "LikedPostID" });
            DropColumn("dbo.CommentsLikes", "UserId");
            DropColumn("dbo.PostsLikes", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostsLikes", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.CommentsLikes", "UserId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.PostsLikes");
            DropPrimaryKey("dbo.CommentsLikes");
            DropColumn("dbo.PostsLikes", "UserName");
            DropColumn("dbo.CommentsLikes", "UserName");
            AddPrimaryKey("dbo.PostsLikes", new[] { "UserId", "LikedPostID" });
            AddPrimaryKey("dbo.CommentsLikes", new[] { "UserId", "LikedCommentID" });
        }
    }
}
