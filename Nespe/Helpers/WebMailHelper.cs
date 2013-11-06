﻿using System;
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
                var message = "Hello" +
                                "The " + @String.Format("{0:d}", StartDate) + " we'll receive " + LastName + " " + FirstName +
                                " in room " + local + " " +
                                "For this date, we will need :" +
                                ((info.Laptop == true) ? " - a laptop" : "" )+
                                ((info.DockingStation == true) ? " - a docking station " : "" )+
                                ((info.Keyboard == true) ? " - a keyboard " : "" )+
                                ((info.Desktop == true) ? " - a desktop " : "" )+
                                ((info.Screen == true) ? " - a screen " : "") +
                                ((info.Mouse == true) ? " - une souris " : "" )+
                                " Comment : " + info.Comment + 
                                "Thank you";
			
                           
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
                        new XElement("coreService", new XCData("PRODUCTIVITY & COLLABORATION/WORKSTATION/LAPTOP")),
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
                    
                    
					var subject="[NESPE] Telephone request for newcomer";
                    var message = " Hello, <br>" +
                                 "The " + @String.Format("{0:d}", StartDate) + " we receive " + LastName + " " + FirstName +
                                 " in room " + local + ".<br><br>" +
                                 "For this date, we will need : <br>" +
                                 ((info.fixPhone == true) ? " - a fix phone <br>" : "" )+
                                 ((info.cordless == true) ? " - a Cordless <br>" : "" )+
                                 ((info.mobile == true) ? " - a mobile phone <br>" : "" )+
                                 ((info.smartphone == true) ? " - a Smartphone <br>" : "" )+
                                 ((info.headphoneForFix == true) ? " - a headset for fix phone <br>" : "") +
                                 ((info.freeHandsForFix == true) ? " - a free hands for fix phone <br>" : "" )+
                                 ((info.freeHandsForCordless == true) ? " - a free hands for cordless <br>" : "" )+
                                 " <br>Comment : " + info.Comment + 
                                 "<br><br> Thank you";
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
                    
                    var subject="[NESPE] Request SAP Roles";
					var message="Hello, <br>"+
                                 "The " + @String.Format("{0:d}", StartDate) + " we'll receive " + LastName + " " + FirstName +
                                 " in quality of " + function + " in department "+ department +". <br><br>" +
                                 LastName + " " + FirstName + " , employee number : "+ EmployeeNumber + " will need <br>" +
                                 ((info.purchaseType == PurchaseTypeRoleSAPEnum.Approver) ? " - Role of purchase Approver <br>" : "") +
                                 ((info.purchaseType == PurchaseTypeRoleSAPEnum.Requester) ? " - Role of purchase Requester <br>" : "") + 
                                 ((info.pCard == true) ? "  - a pCard <br>" : "") +
                                 " Comment : "+ info.Comment +
                                 "<br> Thank you";
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
            var BusinessStream = request.BusinessStream;
            var SID = request.Person.SID;
            var Entity = request.Department.Entity;
			var from = "martin.airas@rdor.nestle.com";
			var to="martin.airas@rdor.nestle.com";
                try {

               

                    var subject = "[NESPE] Request of PMO";
                    var message = " Hello, <br>"+
                                 "The " + @String.Format("{0:d}", StartDate) + " we'll receive " + LastName + " " + FirstName +
                                 " in quality of " + function + " in department "+ department +". <br>" +
                                 "<br> Necessary informations : <br>" +
                                 "First Name : " + FirstName +
                                 "<br> Name : " + LastName +
                                 "<br> Employee Number : " + EmployeeNumber +
                                 "<br> Entity : " + Entity +
                                 "<br> Arrival Date : " + @String.Format("{0:d}", StartDate) +
                                 "<br>Function : " + function +
                                 "<br>Business Stream : " + BusinessStream +
                                 "<br>Alias : " + SID + "<br> <br><br>" +
                                  LastName + " " + FirstName + " will need : <br>" +
                                 ((info.dms == true) ? " DMS <br>" : "" )+ 
                                 ((info.cats == true) ? " CATS <br>" : "") + 
                                 ((info.npdi == true) ? " NPDI <br>" : "") +
                                 ((info.projectManager == true) ? "NESTMS as Project Manager <br>" : "" )+
                                 ((info.ppOperator == true) ? "NESTMS as Pilot Plant operator <br>" : "" )+
                                 ((info.ppLineManager == true) ? "NESTMS as Pilot Plant line Manager <br>" : "" )+
                                 ((info.labTech == true) ? "NESTMS as Lab technician <br>" : "" )+
                                 "<br> Comment : "+ info.Comment +
                                 "<br> Thank you";
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


                    var subject = "[NESPE] Request of mail case";
                    var message = "Hello, <br>"+
                                 "The " + @String.Format("{0:d}", StartDate) + " we will receive " + LastName + " " + FirstName +
                                 " in quality of " + function + " in department "+ department +". <br>" +
                                 LastName + " " + FirstName + " will need a mail case, can you please do the necessary <br>" +
                                 " Comment :"+ info.Comment +
                                   "<br> Thank you";
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

                    var subject = "[NESPE] Request of work equipment";
                    var message = "Hello, <br>"+
                                 "The " + @String.Format("{0:d}", StartDate) + " we will receive " + LastName + " " + FirstName +
                                 " in quality of " + function + " in department "+ department +"which is part of"+ "entity" +". <br>" +
                                 LastName + " " + FirstName + " will need :<br>" +
                                 ((info.secuShoes == true) ? " - Security shoes <br>" : "") +
                                 ((info.shoeSize >0) ? "" : "in size : " + info.shoeSize + "<br>" ) +
                                 ((info.gloves == true) ? " - Gloves " : "" )+
                                 ((info.glovesSize == null) ? "" : "in size : " + info.glovesSize + "<br>") +
                                 ((info.winterCoat == true) ? " - winter coat " : "") +
                                 ((info.winterCoatSize == null) ? "" : "in size : " + info.winterCoatSize + "<br>") +
                                 ((info.polar == true) ? " - Polar " : "") +
                                 ((info.polarSize == null) ? "" : "in size : " + info.polarSize + "<br>") +
                                 ((info.bardushClothes == true) ? " - Bardush clothes " : "") +
                                 ((info.bardusClothesSize == null) ? "" : "in size : " + info.bardusClothesSize + "<br>") +
                                 "Other comments :"+ info.Comment;
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

                    var subject ="[NESPE] Locker Request";
                    var message ="Hello,<br> "+
                                 "The " + @String.Format("{0:d}", StartDate) + " we will receive " + LastName + " " + FirstName +
                                 " in quality of " + function + " in department "+ department +" in room"+ local +". <br><br>" +
                                 LastName + " " + FirstName + " will need a locker <br>" +
                                 "Comment : "+ info.Comment +
                                 "<br> Thanks to do the necessary";
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

                    var subject ="[NESPE] Request newcomer introduction";
                    var message ="Hello, "+
                                 "The " + @String.Format("{0:d}", StartDate) + " we will receive " + LastName + " " + FirstName +
                                 " in quality of " + function + " in department "+ department +". <br><br>" +
                                 " To present your department please contact the newcommer speaking in " + info.introLanguage + "<br>" +
                                 "Comment : "+ info.Comment +
                                 "<br>thank you";
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
        var function = request.Function;
        var local = request.Local;
        var eMail=request.Person.EMail;
        var from = "martin.airas@rdor.nestle.com";
	    var to="martin.airas@rdor.nestle.com";
          try {      

				var subjectAssistant="[NESPE] Request Newcomer";
				var messageAssistant="Hello,<br> "+
                                "The " + @String.Format("{0:d}", StartDate) + " you will receive a new employee at your department : " + LastName + " " + FirstName +
                                " in room " + local+". <br><br>" +
                                "To be sure that everything will be ready when newcomer arrives, please complete the form you'll find in that link (link) <br>"+
                                "<br>Thank you";

                    SendEMail(to, messageAssistant, subjectAssistant,from);
                    
                    var subjectChefDept = "[NESPE] Newcomer Request";
                    var messageChefDept = "Hello, <br>"+
                                "<br>The " + @String.Format("{0:d}", StartDate) + " you will have a newcomer in your department : " + LastName + " " + FirstName +
                                " in quality of "+ function +" in room " + local + ".<br><br>"+
                                "You will receive a copy of the diferents requests of material that will be done by the assistants of your department"+
                                "you can follow at every moment the status of the arrival preparation in this page (link)"+
                                "<br><br>Best regards";
                    SendEMail(to, messageChefDept, subjectChefDept,from);
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