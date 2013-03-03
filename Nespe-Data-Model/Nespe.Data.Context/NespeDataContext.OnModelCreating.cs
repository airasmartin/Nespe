using System;
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
    public partial class NespeDataContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            OnModelCreating_Entities(modelBuilder: modelBuilder);
        }

        private void OnModelCreating_Entities(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().Map(
                m =>
                {
                    m.Properties(t => new { t.Id, t.Version/*, t.ParentId*/, t.Name, t.Description, t.SID, t.Phone, t.EMail, t.Entity });
                    m.ToTable(typeof(Department).Name);
                }
            );
            modelBuilder.Entity<Department>().HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Department>().Property(t => t.Version).IsConcurrencyToken();
            modelBuilder.Entity<Department>().Property(t => t.SID).HasMaxLength(256);
            modelBuilder.Entity<Department>().Property(t => t.Name).HasMaxLength(512);
            modelBuilder.Entity<Department>().Property(t => t.Description).IsMaxLength();
            modelBuilder.Entity<Department>().Property(t => t.Phone).HasMaxLength(124);
            modelBuilder.Entity<Department>().Property(t => t.EMail).HasMaxLength(512);
            modelBuilder.Entity<Department>().Property(t => t.Entity).HasMaxLength(1024);
            modelBuilder.Entity<Department>().HasMany(t => t.PersonList).WithRequired(t => t.Department);

            modelBuilder.Entity<Person>().Map(
                m =>
                {
                    m.Properties(t => new { t.Id, t.Version, t.FirstName, LastName = t.LastName, t.SID, t.Phone, t.EMail });
                    m.ToTable(typeof(Person).Name);
                }
            );
            modelBuilder.Entity<Person>().HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Person>().Property(t => t.Version).IsConcurrencyToken();
            modelBuilder.Entity<Person>().Property(t => t.SID).HasMaxLength(256);
            modelBuilder.Entity<Person>().Property(t => t.FirstName).HasMaxLength(256);
            modelBuilder.Entity<Person>().Property(t => t.LastName).HasMaxLength(256);
            modelBuilder.Entity<Person>().Property(t => t.Phone).HasMaxLength(124);
            modelBuilder.Entity<Person>().Property(t => t.EMail).HasMaxLength(512);

            modelBuilder.Entity<PersonDepartment>().Map(
                m =>
                {
                    m.Properties(t => new { t.Id, t.Version, t.Role });
                    m.ToTable(typeof(PersonDepartment).Name);
                }
            );
            modelBuilder.Entity<PersonDepartment>().HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<PersonDepartment>().Property(t => t.Version).IsConcurrencyToken();
            //modelBuilder.Entity<PersonDepartment>().Property(t => t.RoleId).HasColumnName("Role").HasColumnType("smallint");
            //modelBuilder.Entity<PersonDepartment>().Ignore(t => t.Role);
            modelBuilder.Entity<PersonDepartment>().Property(t => t.Role).HasColumnName("Role").HasColumnType("smallint");
            modelBuilder.Entity<PersonDepartment>().HasRequired(t => t.Person).WithMany().Map(t => t.MapKey("Person_Id")).WillCascadeOnDelete(false);
            modelBuilder.Entity<PersonDepartment>().HasRequired(t => t.Department).WithMany().Map(t => t.MapKey("Department_Id")).WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskRequest>().Map(
                m =>
                {
                    m.Properties(t => new { t.Id, t.Version, t.Name, t.Status, t.TypeId, t.Rank, t.Date, t.Comment });
                    m.ToTable(typeof(TaskRequest).Name);
                    m.MapInheritedProperties();
                }
            );
            
            modelBuilder.Entity<TaskRequest>().HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TaskRequest>().Property(t => t.Version).IsConcurrencyToken();
            modelBuilder.Entity<TaskRequest>().Property(t => t.TypeId).HasColumnName("Type_Id");

            modelBuilder.Entity<PersonTaskRequest>().Map(
                m =>
                {
                    //m.Properties(t => new { t.Id, t.Version });
                    m.ToTable(typeof(PersonTaskRequest).Name);
                    //m.MapInheritedProperties();
                }
            );
            modelBuilder.Entity<PersonTaskRequest>().HasRequired(t => t.Person).WithMany().Map(t => t.MapKey("Person_Id")).WillCascadeOnDelete(false);
            modelBuilder.Entity<PersonTaskRequest>().HasRequired(t => t.Department).WithMany().Map(t => t.MapKey("Department_Id")).WillCascadeOnDelete(false);

            modelBuilder.Entity<ArrivalPersonTaskRequest>().Map(
                m =>
                {
                    m.Properties(t => new { t.StartDate, t.BusinessStream, t.EmployeeNumber, t.Function, t.Superior });
                    m.ToTable(typeof(ArrivalPersonTaskRequest).Name);
                    //m.MapInheritedProperties();
                }
            );

            modelBuilder.Entity<TransfertPersonTaskRequest>().Map(
                m =>
                {
                    m.Properties(t => new { t.TransFrom });
                    m.ToTable(typeof(TransfertPersonTaskRequest).Name);
                    //m.MapInheritedProperties();
                }
            );

            modelBuilder.Entity<DeparturePersonTaskRequest>().Map(
                m =>
                {
                    m.Properties(t => new { t.DepartureDate, t.Initials, t.Location, t.IsRetirement });
                    m.ToTable(typeof(DeparturePersonTaskRequest).Name);
                    //m.MapInheritedProperties();
                }
            );


        }

    }
}
