using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Nespe.Spikes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Spike1(args);
            //Spike2(args);
            Spike3(args);
            Console.WriteLine("Press enter");
            Console.ReadLine();
        }
        static void Spike1(string[] args)
        {
            var kvc = System.Configuration.ConfigurationManager.AppSettings;
            foreach (var k in kvc.AllKeys) {
                Console.WriteLine("{0}={1}", k, kvc[k]);
            }
            var csc = System.Configuration.ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings k in csc)
            {
                Console.WriteLine("{0}={1}", k.Name, k.ConnectionString);
            }
        }
        static void Spike2(string[] args)
        {
            using (var db = new System.Data.SqlServerCe.SqlCeConnection(@"Data Source=..\..\..\Nespe\App_Data\ncom.sdf"))
            {

                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = "select 1 ";
                    var v = cmd.ExecuteScalar();
                    Console.WriteLine(v);
                    //using (var dr = cmd.ExecuteReader()) { 

                    //}
                }

            }
        }

        static void Spike3(string[] args)
        {
            using (var cnn = new System.Data.SqlServerCe.SqlCeConnection(@"Data Source=..\..\..\Nespe\App_Data\nespe-ncom.sdf"))
            {
                using (var db = new Nespe.Context.NespeDbContext(cnn, true)) {
                    db.Database.CreateIfNotExists();
                    db.Database.Initialize(true);

                    var drc = db.DepartmentSet;
                    drc.Add(new Models.Department { Name="IT", Description="Information Technologie"});
                    drc.Add(new Models.Department { Name = "IT-Dev", Description = "Information Technologie Development" });
                    drc.Add(new Models.Department { Name = "IT-Infra", Description = "Information Technologie Infrastructures" });
                    db.SaveChanges();
                    var q = (from t in drc select t);
                    foreach (var dr in q) {
                        Console.WriteLine("{0}", dr.Name);
                    }
                }

            }
        }

        static void Spike4(string[] args)
        {
            using (var cnn = new System.Data.SqlServerCe.SqlCeConnection(@"Data Source=..\..\..\Nespe\App_Data\nespe-ncom.sdf"))
            {
                using (var db = new Nespe.Context.NespeDbContext(cnn, true))
                {
                    db.Database.CreateIfNotExists();
                    db.Database.Initialize(true);
                    var q = (from t in db.RequestSet select t);
                    foreach (var dr in q)
                    {
                        Console.WriteLine("{0}", dr.Person.FirstName);
                    }
                }

            }
        }

    }
}
