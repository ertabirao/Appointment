namespace oa.db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblBusiness",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Barangay = c.String(),
                        City = c.String(),
                        NatureOfBusiness = c.String(),
                        WorkSchedule = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblPersonnel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Position = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Role = c.String(),
                        BusinessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBusiness", t => t.BusinessId, cascadeDelete: true)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.tblBusinessServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        BusinessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBusiness", t => t.BusinessId, cascadeDelete: true)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.tblAppointment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PersonnelId = c.Int(),
                        ServiceId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Note = c.String(),
                        Status = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblPersonnel", t => t.PersonnelId)
                .ForeignKey("dbo.tblBusinessServices", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.tblUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PersonnelId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.tblUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ContactNumber = c.String(),
                        JoinedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblAppointment", "UserId", "dbo.tblUser");
            DropForeignKey("dbo.tblAppointment", "ServiceId", "dbo.tblBusinessServices");
            DropForeignKey("dbo.tblAppointment", "PersonnelId", "dbo.tblPersonnel");
            DropForeignKey("dbo.tblBusinessServices", "BusinessId", "dbo.tblBusiness");
            DropForeignKey("dbo.tblPersonnel", "BusinessId", "dbo.tblBusiness");
            DropIndex("dbo.tblAppointment", new[] { "ServiceId" });
            DropIndex("dbo.tblAppointment", new[] { "PersonnelId" });
            DropIndex("dbo.tblAppointment", new[] { "UserId" });
            DropIndex("dbo.tblBusinessServices", new[] { "BusinessId" });
            DropIndex("dbo.tblPersonnel", new[] { "BusinessId" });
            DropTable("dbo.tblUser");
            DropTable("dbo.tblAppointment");
            DropTable("dbo.tblBusinessServices");
            DropTable("dbo.tblPersonnel");
            DropTable("dbo.tblBusiness");
        }
    }
}
