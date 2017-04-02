namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStoredEmails : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO WallAppStoredEmails ( Description, Body, Subject) VALUES ('Email sent to new users','Gracias por unirte a esta gran familia, ya tu cuenta esta configurada y puedes empezar a postear. Un abrazo.','Hola, bienvenido a WallApp')");
        }
        
        public override void Down()
        {
        }
    }
}
