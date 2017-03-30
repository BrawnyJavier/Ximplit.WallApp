namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctiononposts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "content", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "title", c => c.String());
            AlterColumn("dbo.Comments", "Content", c => c.String());
            DropColumn("dbo.Posts", "content");
        }
    }
}
