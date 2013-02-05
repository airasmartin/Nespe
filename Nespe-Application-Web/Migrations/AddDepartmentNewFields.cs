using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace Nespe.Migrations
{
    public partial class AddDepartmentNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("tbl_department", "Head", c => c.String(nullable: true, defaultValue: null));
            AddColumn("tbl_department", "Assistant1", c => c.String(nullable: true, defaultValue: null));
            AddColumn("tbl_department", "Assistant2", c => c.String(nullable: true, defaultValue: null));
            AddColumn("tbl_department", "Assistant3", c => c.String(nullable: true, defaultValue: null));
            
        }

        public override void Down()
        {
            DropColumn("tbl_request", "Kind");
        }
    }
}