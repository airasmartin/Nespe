using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Linq;

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

        public static int NumberOfWorksDays(int days)
        {
            decimal weeks = days / 7;
            int r = (int)Math.Truncate(weeks) * 5;
            r += days % 7;
            return r;
        }
        static void Spike6(string[] args)
        {
            var model = new ArrivalCompleterFormulaireModel
            {
                RequestModel = new ArrivalNewRequestModel
                {
                    FirstName = "Komanda",
                    LastName = "Phanzu",
                }
            };
            Request request = model.RequestModel;
            if (string.IsNullOrWhiteSpace(request.Person.EMail))
                request.Person.EMail = string.Format("{0}.{1}@nestle.com", model.RequestModel.FirstName, model.RequestModel.LastName);
            var title = string.Format("[NESPE] {0} Newcomer Initial", request.Id);
            var message = @"Please install following Hardware for Joao Ramalho in office PK120
  - Laptop
  - Docking station
  - Screen
  - mouse
  - keyboard
  ";
            var xdoc = new XDocument(
                new XElement("Nestle_SM7",
                    new XElement("documentReferences",
                        new XElement("documentType", new XCData("CH LGO Request")),
                        new XElement("sourceSystem", new XCData("ServiceDesk.xsn")),
                        new XElement("sourceFQDN", new XCData("forms.hq.nestle.com")),
                        new XElement("supportGroup", new XCData("CH_HERITAGE_VEV")),
                        new XElement("supportContactTel", new XCData("+41 21 924-3456")),
                        new XElement("supportContactEmail", new XCData("pascal.hungerbuhler@nestle.com"))
                    ),
                    new XElement("assignmentGroup", new XCData("CH_IT SUPPORT WKS_ORB INSTALLATION")),
                    new XElement("primaryContactDetails",
                        new XElement("contactName", new XCData(request.Person.EMail))
                    ),
                    new XElement("initiatorUserId", new XCData("RDAirasMa")),
                    new XElement("title", new XCData(title)),
                    new XElement("description", new XCData(message)),
                    new XElement("impact", new XCData("4")),
                    new XElement("severity", new XCData("4")),
                    new XElement("callbackType", new XCData("Telephone")),
                    new XElement("coreService", new XCData("BUSINESS TO EMPLOYEE (B2E)/WORKSTATION")),
                    new XElement("category", new XCData("request")),
                    new XElement("area", new XCData("request for change")),
                    new XElement("subArea", new XCData("escalate cr")),
                    new XElement("userEnvironment", new XCData("PRODUCTION")),
                    new XElement("attachments"),
                    new XElement("requestLeadTime", new XCData(NumberOfWorksDays(request.RemainingTime.Days).ToString()))
                )
             );
            var filePath = "./NESPE-2012-001-000004.xml";
            xdoc.Save(filePath);
        }
        static void Spike5(string[] args)
        {
            var xdoc = new XDocument(
                new XElement("Nestle_SM7",
                    new XElement("documentReferences",
                        new XElement("documentType", new XCData("CH LGO Request")),
                        new XElement("sourceSystem", new XCData("ServiceDesk.xsn")),
                        new XElement("sourceFQDN", new XCData("forms.hq.nestle.com")),
                        new XElement("supportGroup", new XCData("CH_HERITAGE_VEV")),
                        new XElement("supportContactTel", new XCData("+41 21 924-3456")),
                        new XElement("supportContactEmail", new XCData("pascal.hungerbuhler@nestle.com"))
                    ),
                    new XElement("assignmentGroup", new XCData("CH_IT SUPPORT WKS_ORB INSTALLATION")),
                    new XElement("primaryContactDetails",
                        new XElement("contactName", new XCData("MARCEL.OPPLIGER@NESTLE.COM"))
                    ),
                    new XElement("initiatorUserId", new XCData("RDAirasMa")),
                    new XElement("title", new XCData("[NESPE] 000002 NewComer install")),
                    new XElement("description", new XCData(@"Please install following Hardware for Joao Ramalho in office PK120
  - Laptop
  - Docking station
  - Screen
  - mouse
  - keyboard
  ")),
                    new XElement("impact", new XCData("4")),
                    new XElement("severity", new XCData("4")),
                    new XElement("callbackType", new XCData("Telephone")),
                    new XElement("coreService", new XCData("BUSINESS TO EMPLOYEE (B2E)/WORKSTATION")),
                    new XElement("category", new XCData("request")),
                    new XElement("area", new XCData("request for change")),
                    new XElement("subArea", new XCData("escalate cr")),
                    new XElement("userEnvironment", new XCData("PRODUCTION")),
                    new XElement("attachments"),
                    new XElement("requestLeadTime", new XCData("4"))
                )
             );
            var filePath = "./NESPE-2012-001-000003.xml";
            xdoc.Save(filePath);
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
