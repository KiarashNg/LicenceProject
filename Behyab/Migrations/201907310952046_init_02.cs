namespace Behyab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserves", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reserves", "Username");
        }
    }
}
