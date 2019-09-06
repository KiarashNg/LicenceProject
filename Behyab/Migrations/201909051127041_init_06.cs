namespace Behyab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_06 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "UserTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "UserTypeId");
            AddForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropColumn("dbo.Users", "UserTypeId");
            DropTable("dbo.UserTypes");
        }
    }
}
