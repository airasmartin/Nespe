using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace Nespe.Migrations
{
    public partial class AddRequestIsFinished : DbMigration
    {
        public override void Up()
        {
            AddColumn("tbl_request", "IsFinished", c => c.Boolean(defaultValue: false));
            Sql("UPDATE tbl_request SET IsFinished = false WHERE IsFinished IS NULL");
        }

        public override void Down()
        {
            DropColumn("tbl_request", "IsFinished");
        }
    }
}