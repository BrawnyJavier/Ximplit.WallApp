namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinglikestablesforcommentsandposts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentsLikes",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        LikedCommentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LikedCommentID });
            
            CreateTable(
                "dbo.PostsLikes",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        LikedPostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.LikedPostID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PostsLikes");
            DropTable("dbo.CommentsLikes");
        }
    }
}
