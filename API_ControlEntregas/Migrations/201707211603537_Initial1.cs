namespace API_ControlEntregas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "fkCliente", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "fkCliente", c => c.Int(nullable: false));
        }
    }
}
