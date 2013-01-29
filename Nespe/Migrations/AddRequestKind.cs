using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace Nespe.Migrations
{
    public partial class AddRequestKind : DbMigration
    {
        public override void Up()
        {
            AddColumn("tbl_request", "Kind", c => c.Int(defaultValue:0));
            Sql("UPDATE tbl_request SET Kind = 0 WHERE Kind IS NULL");
        }

        public override void Down()
        {
            DropColumn("tbl_request", "Kind");
        }
    }
}