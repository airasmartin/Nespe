using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Nespe.Models;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Net;

namespace Nespe.Helpers
{
    public static class WebMailHelper
    {

        public static int NumberOfWorksDays(int days)
        {
            decimal weeks = days / 7;
            int r = (int)Math.Truncate(weeks) * 5;
            r += days % 7;
            return r;
        }
        //
        public static bool ValidateIt(AbstractRequestInfo infoRequest)
        {

            return ValidateIt(infoRequest, Nespe.Properties.Settings.Default.IT_VALIDATION_URL);
        }
        public static bool ValidateIt(AbstractRequestInfo infoRequest, string url) 
        {
            var title = string.Format("[NESPE] {0} Newcomer Initial", infoRequest.Request_Id);
            return ValidateIt(infoRequest, url, title);
        }
        public static bool ValidateIt(AbstractRequestInfo infoRequest, string url, string title)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = System.Net.CredentialCache.DefaultCredentials;

                var fn = Nespe.Properties.Settings.Default.IT_VALIDATION_TEMP_FOLDER + "\\RequestInfo.ValideIt." + DateTime.Now.ToString("yyymmddHHMMss") + ".xml";
                wc.DownloadFile(url, fn);

                XPathDocument doc = new XPathDocument(fn);
                XPathNavigator nav = doc.CreateNavigator();
                var nsmgr = new XmlNamespaceManager(nav.NameTable);
                nsmgr.AddNamespace("n", "SM_x0020_Ticket_x0020_Detail_x0020_List");


                var el = nav.SelectSingleNode(string.Format("/n:Report/n:tblMain/n:Detail_Collection/n:Detail[@txtTitle='{0}']", title), nsmgr);
                if (el == null || !el.HasAttributes)
                    return false;
                var txtID = el.GetAttribute("txtID", "");
                var txtCrtAssignee = el.GetAttribute("txtCrtAssignee", "");

                if (string.IsNullOrWhiteSpace(txtID) || string.IsNullOrWhiteSpace(txtCrtAssignee))
                    return false;
                infoRequest.IsValidated = true;
                infoRequest.Executor = txtCrtAssignee;
                infoRequest.Name = txtID;
                return true;
            }catch(Exception){ return false;}
        }

        public static void SendInfo(this Request request, ITRequestInfo info)
        {
            if (info == null || !info.IsRequired)
                return;



            var StartDate = request.StartDate;
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var local = request.Local;
            var function = request.Function;
            var department = request.Department;
            var EmployeeNumber = request.EmployeeNumber;
            var superior = request.Superior;
            var SID = request.Person.SID;
            var transfertFrom = request.TransFrom;
            var BusinessStream = request.BusinessStream;


            //Création du fichier XML pour générer le ticket
            try
            {


                var title = string.Format("[NESPE] {0} Newcomer Initial", request.Id);
                var message = "Bonjour, <br>" +
                                "Le " + @String.Format("{0:d}", StartDate) + " nous accueillerons " + LastName + " " + FirstName +
                                " au local " + local + ".<br><br>" +
                                "Pour cette date, nous aurons besoin de : <br>" +
                                ((info.Laptop == true) ? " - un laptop <br>" : "" )+
                                ((info.DockingStation == true) ? " - une docking station <br>" : "" )+
                                ((info.Keyboard == true) ? " - un clavier <br>" : "" )+
                                ((info.Desktop == true) ? " - un desktop <br>" : "" )+
                                ((info.Screen == true) ? " - un écran <br>" : "") +
                                ((info.Mouse == true) ? " - une souris <br>" : "" )+
                                " <br>Commentaire : " + info.Comment + 
                                "<br><br> Merci";
			
                           
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
                            new XElement("contactName", new XCData(string.IsNullOrWhiteSpace(request.Person.EMail) ? "MARTIN.AIRAS@RDOR.NESTLE.COM" : request.Person.EMail))
                        ),
                        new XElement("initiatorUserId", new XCData("RDAirasMa")),
                        new XElement("title", new XCData(title)),
                        new XElement("description", new XCData(message)),
                        new XElement("impact", new XCData("4")),
                        new XElement("severity", new XCData("4")),
                        new XElement("callbackType", new XCData("Telephone")),
                        new XElement("coreService", new XCData("BUSINESS TO EMPLOYEE (B2E)/WORKSTATION/LAPTOP")),
                        new XElement("category", new XCData("request")),
                        new XElement("area", new XCData("request for change")),
                        new XElement("subArea", new XCData("escalate cr")),
                        new XElement("userEnvironment", new XCData("PRODUCTION")),
                        new XElement("attachments"),
                        new XElement("requestLeadTime", new XCData(NumberOfWorksDays(request.RemainingTime.Days).ToString()))
                    )
                 );
                //var itXmlFolder=System.Configuration.ConfigurationManager.AppSettings[""];

                var filePath = Nespe.Properties.Settings.Default.IT_XML_ELEBORATION_FOLDER+ @"\NESPE-2012-001-"+request.Id+".xml";
                xdoc.Save(filePath);

            }
            catch (Exception ex)
            {
                throw new Exception(@":<b>Sorry - we couldn't save the XML file to confirm your Request.</b>", ex);
            }

        }

        // Envoi des différents emails
        public static void SendInfo(this Request request, TelephoneRequestInfo info) {
			if(info==null || !info.IsRequired)
				return;



            var StartDate = request.StartDate;
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var local = request.Local;
            var function = request.Function;
            var department = request.Department;
            var EmployeeNumber = request.EmployeeNumber;
            var superior = request.Superior;
            var SID = request.Person.SID;
            var transfertFrom = request.TransFrom;
            var BusinessStream = request.BusinessStream;
			var from = "martin.airas@rdor.nestle.com";
			var to="martin.airas@rdor.nestle.com";
                try {
                    
                    
					var subject="[NESPE] Demande télépone New comer";
                    var message = " Bonjour, <br>" +
                                 "Le " + @String.Format("{0:d}", StartDate) + " nous accueillerons " + LastName + " " + FirstName +
                                 " au local " + local + ".<br><br>" +
                                 "Pour cette date, nous aurons besoin de : <br>" +
                                 ((info.fixPhone == true) ? " - un téléphone fixe <br>" : "" )+
                                 ((info.cordless == true) ? " - un Cordless <br>" : "" )+
                                 ((info.mobile == true) ? " - un téléphone mobile <br>" : "" )+
                                 ((info.smartphone == true) ? " - un Smartphone <br>" : "" )+
                                 ((info.headphoneForFix == true) ? " - un casque pour téléphone fixe <br>" : "") +
                                 ((info.freeHandsForFix == true) ? " - un casque pour fixe <br>" : "" )+
                                 ((info.freeHandsForCordless == true) ? " - un kit mains libres pour Cordless <br>" : "" )+
                                 " <br>Commentaire : " + info.Comment + 
                                 "<br><br> Merci";
					SendEMail(to, message, subject, from);
            
                } catch (Exception) {
                    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>");
                }

        }
        public static void SendInfo(this Request request, RoleSAPRequestInfo info) {
			if(info==null || !info.IsRequired)
				return;
            
            var StartDate = request.StartDate;
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var function = request.Function;
            var department = request.Department;
            var EmployeeNumber = request.EmployeeNumber;
            var superior = request.Superior;
            var transfertFrom = request.TransFrom;
            var BusinessStream = request.BusinessStream;
			var from = "martin.airas@rdor.nestle.com";
			var to="martin.airas@rdor.nestle.com";
                try {
                    
                    var subject="[NESPE] Demande Roles SAP New comer";
					var message="Salut, <br>"+
                                 "Le " + @String.Format("{0:d}", StartDate) + " nous accueillerons " + LastName + " " + FirstName +
                                 " en qualité de " + function + " au département "+ department +". <br><br>" +
                                 LastName + " " + FirstName + " , numéro d'employé "+ EmployeeNumber + " aura besoin de <br>" +
                                 ((info.purchaseType == PurchaseTypeRoleSAPEnum.Approver) ? " - Rôle purchase Approver <br>" : "") +
                                 ((info.purchaseType == PurchaseTypeRoleSAPEnum.Requester) ? " - Rôle purchase Requester <br>" : "") + 
                                 ((info.pCard == true) ? "  - une pCard <br>" : "") +
                                 " Commentaire : "+ info.Comment +
                                 "<br> Merci";
					SendEMail(to, message, subject, from);
            
                } catch (Exception) {
                    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>");
                }

        }



        public static void SendInfo(this Request request, PMORequestInfo info) {
			if(info==null || !info.IsRequired)
				return;
            
            var StartDate = request.StartDate;
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var function = request.Function;
            var department = request.Department;
            var EmployeeNumber = request.EmployeeNumber;
            var superior = request.Superior;
            var transfertFrom = request.TransFrom;
            var BusinessStream = request.BusinessStream;
            var SID = request.Person.SID;
            var Entity = request.Department.Entity;
			var from = "martin.airas@rdor.nestle.com";
			var to="martin.airas@rdor.nestle.com";
                try {

               

                    var subject = "[NESPE] Demande PMO New comer";
                    var message = " Salut, <br>"+
                                 "Le " + @String.Format("{0:d}", StartDate) + " nous accueillerons " + LastName + " " + FirstName +
                                 " en qualité de " + function + " au département "+ department +". <br>" +
                                 "<br> Informations nécessaires : <br>" +
                                 "First Name : " + FirstName +
                                 "<br> Name : " + LastName +
                                 "<br> Employee Number : " + EmployeeNumber +
                                 "<br> Entity : " + Entity +
                                 "<br> Arrival Date : " + @String.Format("{0:d}", StartDate) +
                                 "<br> Transfert from : " + transfertFrom +
                                 "<br>Function : " + function +
                                 "<br>Business Stream : " + BusinessStream +
                                 "<br>Alias : " + SID + "<br> <br><br>" +
                                  LastName + " " + FirstName + " aura besoin de : <br>" +
                                 ((info.dms == true) ? " DMS <br>" : "" )+ 
                                 ((info.cats == true) ? " CATS <br>" : "") + 
                                 ((info.npdi == true) ? " NPDI <br>" : "") +
                                 ((info.projectManager == true) ? "NESTMS en tant que Project Manager <br>" : "" )+
                                 ((info.ppOperator == true) ? "NESTMS en tant que Pilot Plant operator <br>" : "" )+
                                 ((info.ppLineManager == true) ? "NESTMS en tant que Pilot Plant line Manager <br>" : "" )+
                                 ((info.labTech == true) ? "NESTMS en tant que Lab technician <br>" : "" )+
                                 "<br> Commentaire : "+ info.Comment +
                                 "<br> Merci";
					SendEMail(to, message, subject, from);
            
                } catch (Exception) {
                    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>");
                }
            }
        public static void SendInfo(this Request request, MailCaseRequestInfo info) {
			if(info==null || !info.IsRequired)
				return;
            
            var StartDate = request.StartDate;
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var function = request.Function;
            var department = request.Department;
			var from = "martin.airas@rdor.nestle.com";
			var to="martin.airas@rdor.nestle.com";
                try {


                    var subject = "[NESPE] Demande Case courrier New comer";
                    var message = "Salut Martine, <br>"+
                                 "Le " + @String.Format("{0:d}", StartDate) + " nous accueillerons " + LastName + " " + FirstName +
                                 " en qualité de " + function + " au département "+ department +". <br>" +
                                 LastName + " " + FirstName + " aura besoin d'une case courrier, peux-tu faire le nécessaire <br>" +
                                 " Commentaire :"+ info.Comment +
                                   "<br> Merci";
					SendEMail(to, message, subject, from);
            
                } catch (Exception) {
                    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>");
                }
            }
        public static void SendInfo(this Request request, ClothesRequestInfo info) {
			if(info==null || !info.IsRequired)
				return;
            
            var StartDate = request.StartDate;
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var function = request.Function;
            var department = request.Department;
			var from = "martin.airas@rdor.nestle.com";
			var to="martin.airas@rdor.nestle.com";
                try {

                    var subject = "[NESPE] Demande Equipement de travail New comer";
                    var message = "Bonjour, <br>"+
                                 "Le " + @String.Format("{0:d}", StartDate) + " nous accueillerons " + LastName + " " + FirstName +
                                 " en qualité de " + function + " au département "+ department +"qui fait partie du"+ "entity" +". <br>" +
                                 LastName + " " + FirstName + " aura besoin de <br>" +
                                 ((info.secuShoes == true) ? " - Chaussures de sécurité <br>" : "") +
                                 ((info.shoeSize >0) ? "" : "en taille : " + info.shoeSize + "<br>" ) +
                                 ((info.gloves == true) ? " - Gants " : "" )+
                                 ((info.glovesSize == null) ? "" : "en taille : " + info.glovesSize + "<br>") +
                                 ((info.winterCoat == true) ? " - Veste d'hiver " : "") +
                                 ((info.winterCoatSize == null) ? "" : "en taille : " + info.winterCoatSize + "<br>") +
                                 ((info.polar == true) ? " - Polaire " : "") +
                                 ((info.polarSize == null) ? "" : "en taille : " + info.polarSize + "<br>") +
                                 ((info.bardushClothes == true) ? " - Vêtement de travail Bardusch " : "") +
                                 ((info.bardusClothesSize == null) ? "" : "en taille : " + info.bardusClothesSize + "<br>") +
                                 "Autres et Commentaire :"+ info.Comment;
			    SendEMail(to, message, subject, from);
            
                } catch (Exception) {
                    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>");
                }
            }
        public static void SendInfo(this Request request, LockerRequestInfo info) {
			if(info==null || !info.IsRequired)
				return;
            
            var StartDate = request.StartDate;
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var function = request.Function;
            var department = request.Department;
            var local = request.Local;
			var from = "martin.airas@rdor.nestle.com";
			var to="martin.airas@rdor.nestle.com";
                try {

                    var subject ="[NESPE] Demande Casier New comer";
                    var message ="Bonjour,<br> "+
                                 "Le " + @String.Format("{0:d}", StartDate) + " nous accueillerons " + LastName + " " + FirstName +
                                 " en qualité de " + function + " au département "+ department +" au local"+ local +". <br><br>" +
                                 LastName + " " + FirstName + " aura besoin d 'un casier <br>" +
                                 "Commentaire : "+ info.Comment +
                                 "<br> merci de faire le nécessaire";
			    SendEMail(to, message, subject, from);
            
                } catch (Exception) {
                    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>");
                }
            }
        public static void SendInfo(this Request request, IntroRequestInfo info) {
			if(info==null || !info.IsRequired)
				return;
            
            var StartDate = request.StartDate;
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var function = request.Function;
            var department = request.Department;
            var local = request.Local;
            var EmployeeNumber = request.EmployeeNumber;
            var superior = request.Superior;
            var transfertFrom = request.TransFrom;
            var BusinessStream = request.BusinessStream;
            var SID = request.Person.SID;
			var from = "martin.airas@rdor.nestle.com";
			var to="martin.airas@rdor.nestle.com";
                try {

                    var subject ="[NESPE] Demande Introduction New comer";
                    var message ="Bonjour, "+
                                 "Le " + @String.Format("{0:d}", StartDate) + " nous accueillerons " + LastName + " " + FirstName +
                                 " en qualité de " + function + " au département "+ department +". <br><br>" +
                                 " Afin de lui présenter votre département, veuillez le contacter en " + info.introLanguage + "<br>" +
                                 "Commentaire : "+ info.Comment +
                                 "<br>merci";
                    SendEMail(to, message, subject, from);

                }
                catch (Exception)
                {
                    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>");
                }			
        }
        public static void SendEmailCreation(Request request)
        {
            
        var LastName = request.Person.LastName;
        var FirstName = request.Person.FirstName;
        var StartDate = request.StartDate;
        var local = request.Local;
        var eMail=request.Person.EMail;
        var from = "martin.airas@rdor.nestle.com";
	    var to="martin.airas@rdor.nestle.com";
          try {      

				var subjectAssistant="[NESPE] Demande New comer";
				var messageAssistant="Hello,<br> "+
                                "Le " + @String.Format("{0:d}", StartDate) + " tu auras dans ton département l'arrivée de " + LastName + " " + FirstName +
                                " au local " + local+". <br><br>" +
                                "Afin que tout soit prêt lors de son arrivée, tu es priée de remplir le formulaire"+
                                " que tu trouveras sur (lien page)<br>"+
                                "<br>Merci d'avance";

                    SendEMail(to, messageAssistant, subjectAssistant,from);
                    
                    var subjectParrain = "[NESPE] Demande New comer Parrain";
                    var messageParrain = "Hello, <br>"+
                                "Le " + @String.Format("{0:d}", StartDate) + " tu auras dans ton département l'arrivée de " + LastName + " " + FirstName +
                                " au local " + local + ".<br><br>"+
                                "Afin que tout soit prêt et que son arrivée se fasse avec le meilleur accompagnement,"+
                                " tu es prié de contacter"+ "Assistante" + "afin que vous vous répartissiez les tâches que" +
                                "tu trouveras sur (lien page) <br>" + 
                                "<br>Merci d'avance";
                    SendEMail(to, messageParrain, subjectParrain,from);
                }
                catch (Exception)
                {
                    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>");
                }
        }

        public static void SendEMail(string to, string message, string subject, string from = null)
        {

            if (string.IsNullOrWhiteSpace(from))
            {
                var ui = Nespe.Helpers.ActiveDirectoryHelper.GertUserInfo(System.Web.HttpContext.Current.User.Identity.Name);
                if(ui!=null)
					from = ui.EMail;
            }
			if (string.IsNullOrWhiteSpace(from))
				from="martin.airas@rdor.nestle.com";
        
            WebMail.SmtpServer = "smtp.eur.nestle.com";
            WebMail.SmtpPort = 25;
            WebMail.EnableSsl = false;
            WebMail.From = from;

            WebMail.Send(to, subject, message);
        }

    }
}