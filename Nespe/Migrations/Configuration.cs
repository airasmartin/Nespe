namespace Nespe.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Nespe.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Nespe.Context.NespeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Nespe.Context.NespeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.DepartmentSet.AddOrUpdate(
            //  t => t.Name,
            //  new Department { Name = "IT", Description = "Information Technologie" },
            //  new Department { Name = "IT-Dev", Description = "Information Technologie Development" },
            //  new Department { Name = "IT-Infra", Description = "Information Technologie Infrastructures" }
            //);
            //context.PersonSet.AddOrUpdate(
            //  t => t.FirstName,
            //  new Person { FirstName = "Martin", LastName = "Airas" },
            //  new Person { FirstName = "Komanda", LastName = "Phanzu" },
            //  new Person { FirstName = "Mickey", LastName = "Disney" },
            //  new Person { FirstName = "Donald", LastName = "Duck" },
            //  new Person { FirstName = "Lucky", LastName = "Luck" }
            //);
        }
    }
}
