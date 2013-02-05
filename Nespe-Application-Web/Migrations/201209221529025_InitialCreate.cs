namespace Nespe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_request",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonDepartment_Id = c.Long(),
                        Function = c.String(maxLength: 4000),
                        Superior = c.String(maxLength: 4000),
                        BusinessStream = c.String(maxLength: 4000),
                        StartDate = c.DateTime(nullable: false),
                        EmployeeNumber = c.String(nullable: false, maxLength: 4000),
                        nonSAP = c.Boolean(nullable: false),
                        Local = c.String(maxLength: 4000),
                        TransFrom = c.String(maxLength: 4000),
                        Parrain = c.String(maxLength: 4000),
                        Completed = c.Boolean(nullable: false),
                        ActiveDirectoryId = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_person_department", t => t.PersonDepartment_Id)
                .Index(t => t.PersonDepartment_Id);
            
            CreateTable(
                "dbo.tbl_person_department",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Rank = c.Long(nullable: false),
                        Department_Id = c.Long(nullable: false),
                        Person_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_department", t => t.Department_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_person", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.Department_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.tbl_department",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SID = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        EMail = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_person",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SID = c.String(maxLength: 4000),
                        FirstName = c.String(nullable: false, maxLength: 4000),
                        LastName = c.String(nullable: false, maxLength: 4000),
                        EMail = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Initials = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_requestTypeInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbl_person_department", new[] { "Person_Id" });
            DropIndex("dbo.tbl_person_department", new[] { "Department_Id" });
            DropIndex("dbo.tbl_request", new[] { "PersonDepartment_Id" });
            DropForeignKey("dbo.tbl_person_department", "Person_Id", "dbo.tbl_person");
            DropForeignKey("dbo.tbl_person_department", "Department_Id", "dbo.tbl_department");
            DropForeignKey("dbo.tbl_request", "PersonDepartment_Id", "dbo.tbl_person_department");
            DropTable("dbo.tbl_requestTypeInfo");
            DropTable("dbo.tbl_person");
            DropTable("dbo.tbl_department");
            DropTable("dbo.tbl_person_department");
            DropTable("dbo.tbl_request");
        }
    }
}
