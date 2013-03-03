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
            //Spike_SqlServerCe_Upgrade_To_Current(args);
            //Spike_SqlCeConnection(args);
            //Spike_SqlCeConnection_With_ConnectionString(args);
            //Spike_OleDbConnection_With_ConnectionString(args);
            //Spike_NespeDataContext_ExternalConnection_Owned(args);
            //Spike_ListDbProviders(args);
            //Spike_InitialContext(args);
            //Spike_NespeDataContext_Department_List(args);
            //Spike_NespeDataContext_Person_List(args);
            Spike_NespeDataContext_PersonDepartment_List(args);
            Console.WriteLine("Press ENTER...");
            Console.ReadLine();
            
        }

        private static void Spike_NespeDataContext_PersonDepartment_List(string[] args)
        {
            using (var ctx = new Nespe.Data.Context.NespeDataContext())
            {
                foreach (var dp in ctx.PersonDepartmentSet)
                {
                    Console.WriteLine(dp);
                };

            }
        }

        private static void Spike_NespeDataContext_Person_List(string[] args)
        {
            using (var ctx = new Nespe.Data.Context.NespeDataContext())
            {
                foreach (var dp in ctx.PersonSet)
                {
                    Console.WriteLine(dp);
                };

            }
        }

        private static void Spike_NespeDataContext_Department_List(string[] args)
        {
            using (var ctx = new Nespe.Data.Context.NespeDataContext())
            {
                foreach (var dp in ctx.DepartmentSet)
                {
                    Console.WriteLine(dp);
                };

            }
        }

        private static void Spike_SqlServerCe_Upgrade_To_Current(string[] args)
        {
            var e = new System.Data.SqlServerCe.SqlCeEngine(Nespe.Data.Context.NespeDataContext.ConnectionString);
            e.Upgrade();

        }
        
        private static void Spike_ListDbProviders(string[] args)
        {

            var pf = System.Data.Common.DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0");
            var cnn=pf.CreateConnection();
        }

        static void Spike_InitialContext(string[] args)
        {
            using (var ctx = new Nespe.Data.Context.NespeDataContext())
            {
                ctx.Database.CreateIfNotExists();
                ctx.DepartmentSet.Add(
                    new Department
                    {
                        Name = "IT",
                        Description = " Information Technologies",
                        PersonList = new List<PersonDepartment> { 
                            new PersonDepartment{ 
                                Person=new Person{
                                    FirstName="Martin", LastName="Airas"
                                },
                                Role=PersonDepartmentRoleEnum.Head
                            } 
                        }
                    }
                  );
                ctx.PersonDepartmentSet.Add(
                    new PersonDepartment
                    {
                        Role = PersonDepartmentRoleEnum.Head,
                        Person = new Person
                        {
                            FirstName = "Martin",
                            LastName = "Airas"
                        },
                        Department = new Department
                        {
                            Name = "IT",
                            Description = " Information Technologies"
                        }
                    });
                ctx.SaveChanges();
                foreach (var dp in ctx.DepartmentSet) {
                    Console.WriteLine(dp);
                };
            }
        }
        static void Spike_NespeDataContext_ExternalConnection_Owned(string[] args)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["NespeDataContext"];
            using (var cnn = new System.Data.SqlServerCe.SqlCeConnection(cs.ConnectionString))
            {
                using (var ctx = new Nespe.Data.Context.NespeDataContext(cnn, true))
                {
                    ctx.Database.CreateIfNotExists();
                    foreach (var dp in ctx.DepartmentSet)
                    {
                        Console.WriteLine(dp);
                    };
                }
            }
        }

        private static void Spike_OleDbConnection_With_ConnectionString(string[] args)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["NespeDataContext"];
            using (var cnn = new System.Data.OleDb.OleDbConnection(cs.ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select 1+1";
                    cnn.Open();
                    var v = cmd.ExecuteScalar();
                    Console.WriteLine(v);
                }
            }
        }
        private static void Spike_SqlCeConnection_With_ConnectionString(string[] args)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["NespeDataContext"];
            using (var cnn = new System.Data.SqlServerCe.SqlCeConnection(cs.ConnectionString))
            {
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select 1+1";
                    cnn.Open();
                    var v = cmd.ExecuteScalar();
                    Console.WriteLine(v);
                }
            }
        }
        static void Spike_SqlCeConnection(string[] args)
        {
            using (var cnn = new System.Data.SqlServerCe.SqlCeConnection(@"Data Source=D:\workspace\_NESPE\Nespe\data\db_nespe.sdf")) 
            {
                using (var cmd = cnn.CreateCommand()) 
                {
                    cmd.CommandText = "select 1+1";
                    cnn.Open();
                    var v=cmd.ExecuteScalar();
                    Console.WriteLine(v);
                }
            }
        }

    }
}
