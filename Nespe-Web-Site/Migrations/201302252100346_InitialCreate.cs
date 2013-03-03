namespace Nespe.Application.WebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Version = c.Int(),
                        SID = c.String(maxLength: 256),
                        Name = c.String(maxLength: 512),
                        Description = c.String(),
                        Entity = c.String(maxLength: 1024),
                        EMail = c.String(maxLength: 512),
                        Phone = c.String(maxLength: 124),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonDepartment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Version = c.Int(),
                        Role = c.Short(nullable: false),
                        Person_Id = c.Long(nullable: false),
                        Department_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.Department_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Version = c.Int(),
                        SID = c.String(maxLength: 256),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        EMail = c.String(maxLength: 512),
                        Phone = c.String(maxLength: 124),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PersonDepartment", new[] { "Department_Id" });
            DropIndex("dbo.PersonDepartment", new[] { "Person_Id" });
            DropForeignKey("dbo.PersonDepartment", "Department_Id", "dbo.Department");
            DropForeignKey("dbo.PersonDepartment", "Person_Id", "dbo.Person");
            DropTable("dbo.Person");
            DropTable("dbo.PersonDepartment");
            DropTable("dbo.Department");
        }
    }
}
