namespace TestLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Role", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Role", c => c.Int(nullable: false));
        }
    }
}
