namespace LooksAliceWebAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComprasStatusEnvio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "StatusEnvio", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "StatusEnvio");
        }
    }
}
