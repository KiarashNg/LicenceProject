namespace Behyab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_07 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reserves", "UserCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reserves", "UserCode", c => c.String());
        }
    }
}
