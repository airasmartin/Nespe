using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Common;
using Nespe.Models;
using System.Data.EntityClient;

namespace Nespe.Context
{
    [DbModelBuilderVersion(DbModelBuilderVersion.V5_0_Net4)]
    public partial class NespeDbContext : DbContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new NespeDbContext object using the connection string found in the 'NespeDbContext' section of the application configuration file.
        /// </summary>
        public NespeDbContext() : this("name=NespeDbContext"){}
    
        /// <summary>
        /// Initialize a new NespeDbContext object.
        /// </summary>
        public NespeDbContext(string connectionString) : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            OnContextCreated();
        }
        public NespeDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            this.Configuration.LazyLoadingEnabled = false;
            OnContextCreated();
        }

        #endregion

    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Configurations.Add(new System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PersonDepartment> {  });
            //modelBuilder.Entity<PersonDepartment>().Property(p => p.RoleValue).HasColumnType("int").HasColumnName("Role");

            modelBuilder.Entity<Person>().HasKey(t => t.Id);
            
            modelBuilder.Entity<Department>().HasKey(t => t.Id);

            //modelBuilder.Entity<PersonDepartment>().Map(mc => {
            //    //mc.MapInheritedProperties();
            //    //mc.Properties(p => new { p.Id, p.Rank, p.Person_Id, p.Department_Id });
            //    mc.ToTable("tbl_person_department");
            //});
            
            modelBuilder.Entity<PersonDepartment>().HasKey(t => t.Id);
            //modelBuilder.Entity<PersonDepartment>().HasMany(t=>t.Person).
            //modelBuilder.Entity<PersonDepartment>().HasRequired<Person>(t => t.Person).WithMany().HasForeignKey(t=>t.Person_Id).WillCascadeOnDelete(false);
            //modelBuilder.Entity<PersonDepartment>().HasRequired<Person>(t => t.Person).WithRequiredDependent().Map(mc => { mc.MapKey("Person_Id").MapKey("Id").ToTable("tbl_person_department"); });
            //modelBuilder.Entity<PersonDepartment>().HasRequired<Department>(t => t.Department).WithMany().HasForeignKey(t => t.Department_Id).WillCascadeOnDelete(false);
            //modelBuilder.Configurations.Add(modelBuilder.Entity<PersonDepartment>().);
            //modelBuilder.Entity<Person>().HasOptional<PersonDepartment>(t=>t.).WithRequiredDependent(t => t.Person_Id);
            //modelBuilder.Entity<PersonDepartment>().Property<long>(t => t.Person.Id).HasColumnName("Person_id");
            
            //modelBuilder.Entity<PersonDepartment>().HasRequired<Person>(t=>t.Person).WithRequiredDependent().Map();
            
            
        }
        public DbSet<Request> RequestSet { get; set; }
        public DbSet<RequestTypeInfo> RequestInfoSet { get; set; }
        public DbSet<Person> PersonSet { get; set; }
        public DbSet<Department> DepartmentSet { get; set; }
        public DbSet<PersonDepartment> PersonDepartmentSet { get; set; }
    }
}
