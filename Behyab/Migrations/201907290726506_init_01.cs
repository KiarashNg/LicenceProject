namespace Behyab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        ExpertyId = c.Int(nullable: false),
                        ExpCode = c.String(nullable: false),
                        ClinicId = c.Int(nullable: false),
                        WeekId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId, cascadeDelete: true)
                .ForeignKey("dbo.Experties", t => t.ExpertyId, cascadeDelete: true)
                .ForeignKey("dbo.Weeks", t => t.WeekId)
                .Index(t => t.ExpertyId)
                .Index(t => t.ClinicId)
                .Index(t => t.WeekId);
            
            CreateTable(
                "dbo.Experties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Weeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpertCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reserves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(nullable: false),
                        ClinicId = c.Int(),
                        ExpertyId = c.Int(),
                        DoctorId = c.Int(),
                        UserCode = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Experties", t => t.ExpertyId)
                .Index(t => t.ClinicId)
                .Index(t => t.ExpertyId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Code = c.String(nullable: false),
                        Mobile = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reserves", "ExpertyId", "dbo.Experties");
            DropForeignKey("dbo.Reserves", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Reserves", "ClinicId", "dbo.Clinics");
            DropForeignKey("dbo.Doctors", "WeekId", "dbo.Weeks");
            DropForeignKey("dbo.Doctors", "ExpertyId", "dbo.Experties");
            DropForeignKey("dbo.Doctors", "ClinicId", "dbo.Clinics");
            DropIndex("dbo.Reserves", new[] { "DoctorId" });
            DropIndex("dbo.Reserves", new[] { "ExpertyId" });
            DropIndex("dbo.Reserves", new[] { "ClinicId" });
            DropIndex("dbo.Doctors", new[] { "WeekId" });
            DropIndex("dbo.Doctors", new[] { "ClinicId" });
            DropIndex("dbo.Doctors", new[] { "ExpertyId" });
            DropTable("dbo.Users");
            DropTable("dbo.Reserves");
            DropTable("dbo.ExpertCodes");
            DropTable("dbo.Weeks");
            DropTable("dbo.Experties");
            DropTable("dbo.Doctors");
            DropTable("dbo.Clinics");
        }
    }
}
