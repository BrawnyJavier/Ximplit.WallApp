namespace WallApp.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populatingStoredEmails : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.WallAppStoredEmails");
            AddColumn("dbo.WallAppStoredEmails", "EmailCode", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.WallAppStoredEmails", "EmailCode");
            DropColumn("dbo.WallAppStoredEmails", "EmailId");
            Sql("INSERT INTO WallAppStoredEmails (EmailCode, Description, Body, Subject) VALUES ('WELCOME','Email sent to new users','Gracias por unirte a esta gran familia, ya tu cuenta esta configurada y puedes empezar a postear. Un abrazo.','Hola, bienvenido a WallApp')");
            Sql("INSERT INTO WallAppStoredEmails (EmailCode, Description, Body, Subject) VALUES ('PASSWORD','Email sent to a user with his password if the system detects he does not remember it.','Alguien (ojalá y hayas sido tu) ha solicitado tu contraseña, si no haz sido tu, por favor ignora este mensaje, pero de lo contrario, aquí te la enviamos para que puedas iniciar sesión en tu app favorita.','Hmm, ¿haz olvidado tu contraseña?')");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WallAppStoredEmails", "EmailId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.WallAppStoredEmails");
            DropColumn("dbo.WallAppStoredEmails", "EmailCode");
            AddPrimaryKey("dbo.WallAppStoredEmails", "EmailId");
        }
    }
}
