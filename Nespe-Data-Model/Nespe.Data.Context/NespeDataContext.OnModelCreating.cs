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
                    m.Properties(t => new { t.Id, t.Version/*, t.ParentId*/, t.Name, t.Description, t.SID, t.Entity });
                    m.ToTable(typeof(Department).Name);
                }
            );
            modelBuilder.Entity<Department>().HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Department>().Property(t => t.Version).IsConcurrencyToken();
            modelBuilder.Entity<Department>().Property(t => t.SID).HasMaxLength(256);
            modelBuilder.Entity<Department>().Property(t => t.Name).HasMaxLength(512);
            modelBuilder.Entity<Department>().Property(t => t.Description).IsMaxLength();
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
            modelBuilder.Entity<PersonDepartment>().HasRequired(t => t.Person).WithMany().Map(t => t.MapKey("Person_Id")).WillCascadeOnDelete(true);
            modelBuilder.Entity<PersonDepartment>().HasRequired(t => t.Department).WithMany().Map(t => t.MapKey("Department_Id")).WillCascadeOnDelete(false);

        }

    }
}
