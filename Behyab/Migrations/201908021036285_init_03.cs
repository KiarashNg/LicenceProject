namespace Behyab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clinics", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Experties", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Weeks", "Day", c => c.String(nullable: false));
            AlterColumn("dbo.ExpertCodes", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExpertCodes", "Code", c => c.String());
            AlterColumn("dbo.Weeks", "Day", c => c.String());
            AlterColumn("dbo.Experties", "Name", c => c.String());
            AlterColumn("dbo.Clinics", "Name", c => c.String());
        }
    }
}
