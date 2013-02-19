using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nespe.Data.Entities;

namespace Nespe.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            Spike_InitialContext(args);
        }
        static void Spike_InitialContext(string[] args)
        {
            using (var ctx = new Nespe.Data.Context.NespeDataContext())
            {
                ctx.Database.CreateIfNotExists();
                //ctx.DepartmentSet.Add(
                //    new Department { 
                //        Name = "IT", Description = " Information Technologies", 
                //        PersonList = new List<PersonDepartment> { 
                //            new PersonDepartment{ 
                //                Person=new Person{
                //                    FirstName="Martin", LastName="Airas"
                //                },
                //                Role=PersonDeparmentRoleEnum.Head
                //            } 
                //        } 
                //    }
                //  );
                //var e = new System.Data.SqlServerCe.SqlCeEngine(Nespe.Data.Context.NespeDataContext.ConnectionString);
                //e.Upgrade();
                //ctx.PersonDepartmentSet.Add(
                //    new PersonDepartment
                //    {
                //        Role = PersonDeparmentRoleEnum.Head,
                //        Person = new Person
                //        {
                //            FirstName = "Martin",
                //            LastName = "Airas"
                //        },
                //        Department = new Department
                //        {
                //            Name = "IT",
                //            Description = " Information Technologies"
                //        }
                //    });
                //ctx.SaveChanges();
                foreach (var dp in ctx.DepartmentSet) {
                    Console.WriteLine(dp);
                };
            }
        }
    }
}
