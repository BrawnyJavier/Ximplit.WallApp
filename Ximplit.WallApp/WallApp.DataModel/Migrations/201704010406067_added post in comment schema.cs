namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpostincommentschema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Post_PostId", c => c.Int());
            CreateIndex("dbo.Comments", "Post_PostId");
            AddForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts", "PostId");
            DropColumn("dbo.Posts", "title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "title", c => c.String());
            DropForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_PostId" });
            DropColumn("dbo.Comments", "Post_PostId");
        }
    }
}
