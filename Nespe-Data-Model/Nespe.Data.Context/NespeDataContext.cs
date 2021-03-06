﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Entity;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using Nespe.Data.Entities;

namespace Nespe.Data.Context
{
    [DbModelBuilderVersion(DbModelBuilderVersion.V5_0)]
    public partial class NespeDataContext : DbContext
    {
        public NespeDataContext()
            : base("name=NespeDataContext")
        {
        }

        public NespeDataContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Department> DepartmentSet { get; set; }
        public DbSet<Person> PersonSet { get; set; }
        public DbSet<PersonDepartment> PersonDepartmentSet { get; set; }
        public DbSet<TaskRequest> TaskRequestSet { get; set; }
        public DbSet<PersonTaskRequest> PersonTaskRequestSet { get; set; }
        public DbSet<ArrivalPersonTaskRequest> ArrivalPersonTaskRequestSet { get; set; }
        public DbSet<DeparturePersonTaskRequest> DeparturePersonTaskRequestSet { get; set; }
        public DbSet<TransfertPersonTaskRequest> TransfertPersonTaskRequestSet { get; set; }
        public static string ConnectionString { get { return Nespe.Data.Properties.Settings.Default.NespeDataContext; } }
        public static DbConnection CreateConnection() { return new OleDbConnection(ConnectionString); }

    }
}
