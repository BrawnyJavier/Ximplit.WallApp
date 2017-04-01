namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingtitlesfrompostsschema : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "title", c => c.String(nullable: false));
        }
    }
}
