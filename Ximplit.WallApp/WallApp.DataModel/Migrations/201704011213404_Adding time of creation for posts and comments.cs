namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingtimeofcreationforpostsandcomments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Posts", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "CreationDate");
            DropColumn("dbo.Comments", "CreationDate");
        }
    }
}
