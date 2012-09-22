using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Common;
using Nespe.Models;

namespace Nespe.Context
{
    public class NespeDbContext : DbContext
    {
        public NespeDbContext()
            : base()
        {

        }
        public NespeDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            :base(existingConnection, contextOwnsConnection)
        {

        }
        public DbSet<Request> RequestSet { get; set; }
        public DbSet<RequestTypeInfo> RequestInfoSet { get; set; }
        public DbSet<Person> PersonSet { get; set; }
        public DbSet<Department> DepartmentSet { get; set; }
        public DbSet<PersonDepartment> PersonDepartmentSet { get; set; }
    }
}
