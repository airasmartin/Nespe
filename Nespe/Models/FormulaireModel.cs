using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
//using System.Data.Linq.Mapping;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace Nespe.Models
{

    public class AbstractFormulaireModel<T>
        where T : AbstractNewRequestModel
    {
        [Key()]
        public long? Id { get; set; }
        public T RequestModel { get; set; }
        public DateTime dateCompleted { get; set; }
    }
    public abstract class AbstractRequestInfo
    {
        [Key()]
        public long Id { get; set; }
        public long Request_Id { get; set; }
        public long Type_Id { get; set; }
        [NotMapped]
        public virtual string StaticName { get; set; }
        [NotMapped]
        public virtual string StaticDescription { get; set; }
        public string Name { get; set; }
        public string Executor { get; set; }
        public StatusRequestInfoEnum Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public bool IsClosed { get { return Date != null && Date.Subtract(DateTime.MinValue).Ticks > 100 && !string.IsNullOrWhiteSpace(Executor); } }
        [NotMapped]
        public bool IsValidated { get; set; }
        [Display(Name = "Comment:")]
        public string Comment { get; set; }
        public bool IsRequired { get; set; }
        public virtual T Copy<T>(T src, bool copyId = false) where T : AbstractRequestInfo
        {
            var dst = this;
            Copy(dst, src, copyId);
            return src;
        }

        public static void Copy(AbstractRequestInfo dst, AbstractRequestInfo src, bool copyId = false)
        {
            if(copyId)dst.Id = src.Id;
            dst.Request_Id = src.Request_Id;
            dst.Type_Id = src.Type_Id;
            dst.Name = src.Name;
            dst.Executor = src.Executor;
            dst.Status = src.Status;
            dst.Date = src.Date;
            dst.Comment = src.Comment;
            dst.IsValidated = src.IsValidated;
        }
    }
    [Table("ITRequestInfo")]
    public class ITRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "IT Hardware" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        [Display(Name = "Laptop")]
        public bool Laptop { get; set; }
        [Display(Name = "Docking Station")]
        public bool DockingStation { get; set; }
        [Display(Name = "Keyboard")]
        public bool Keyboard { get; set; }
        [Display(Name = "Desktop")]
        public bool Desktop { get; set; }
        [Display(Name = "Screen")]
        public bool Screen { get; set; }
        [Display(Name = "Mouse")]
        public bool Mouse { get; set; }
        public virtual ITRequestInfo Copy(ITRequestInfo src, bool copyId = false)
        {
            var dst = this;
            dst.Laptop = src.Laptop;
            dst.DockingStation = src.DockingStation;
            dst.Keyboard = src.Keyboard;
            dst.Desktop = src.Desktop;
            dst.Screen = src.Screen;
            dst.Mouse = src.Mouse;
            return base.Copy(src, copyId);
        }

    }
    [Table("TelephoneRequestInfo")]
    public class TelephoneRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Telephone" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        public bool fixPhone { get; set; }
        [Display(Name = "Cordless")]
        public bool cordless { get; set; }
        [Display(Name = "Mobile")]
        public bool mobile { get; set; }
        [Display(Name = "Smartphone")]
        public bool smartphone { get; set; }
        [Display(Name = "Headset for fix phone")]
        public bool headphoneForFix { get; set; }
        [Display(Name = "Headset for Cordless")]
        public bool headphoneForCordless { get; set; }
        [Display(Name = "Free hands for fix")]
        public bool freeHandsForFix { get; set; }
        [Display(Name = "Free hands for Cordless")]
        public bool freeHandsForCordless { get; set; }
        public virtual TelephoneRequestInfo Copy(TelephoneRequestInfo src, bool copyId = false)
        {
            var dst = this;
            dst.fixPhone = src.fixPhone;
            dst.cordless = src.cordless;
            dst.mobile = src.mobile;
            dst.smartphone = src.smartphone;
            dst.headphoneForFix = src.headphoneForFix;
            dst.headphoneForCordless = src.headphoneForCordless;
            dst.freeHandsForFix = src.freeHandsForFix;
            dst.freeHandsForCordless = src.freeHandsForCordless;
            return base.Copy(src, copyId);
        }
    }
    [Table("RoleSAPRequestInfo")]
    public class RoleSAPRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "SAP Roles" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        [Display(Name = "Purchase type")]
        public PurchaseTypeRoleSAPEnum purchaseType { get; set; }
        [Display(Name = "PCard")]
        public bool pCard { get; set; }
        public virtual RoleSAPRequestInfo Copy(RoleSAPRequestInfo src, bool copyId = false)
        {
            var dst = this;
            dst.purchaseType = src.purchaseType;
            dst.pCard = src.pCard;
            return base.Copy(src, copyId);
        }
    }
    public enum PurchaseTypeRoleSAPEnum
    {
        Undefined, Approver, Requester
    }
    [Table("PMORequestInfo")]
    public class PMORequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "PMO" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        [Display(Name = "DMS")]
        public bool dms { get; set; }
        [Display(Name = "CATS")]
        public bool cats { get; set; }
        [Display(Name = "NPDI")]
        public bool npdi { get; set; }
        [Display(Name = "NESTMS")]
        public bool nestms { get; set; }
        [Display(Name = "Project Manager")]
        public bool projectManager { get; set; }
        [Display(Name = "Pilot Plant Operator")]
        public bool ppOperator { get; set; }
        [Display(Name = "Pilot Plant Manager")]
        public bool ppLineManager { get; set; }
        [Display(Name = "Lab Technician")]
        public bool labTech { get; set; }
        public virtual PMORequestInfo Copy(PMORequestInfo src, bool copyId = false)
        {
            var dst = this;
            dst.dms = src.dms;
            dst.cats = src.cats;
            dst.npdi = src.npdi;
            dst.nestms = src.nestms;
            dst.projectManager = src.projectManager;
            dst.ppOperator = src.ppOperator;
            dst.ppLineManager = src.ppLineManager;
            dst.labTech = src.labTech;
            return base.Copy(src, copyId);
        }
    }
    [Table("PMOCatsInfo")]
    public class PMOCatsRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "PMOCats" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        [Display(Name = "CATS")]
        public bool cats { get; set; }
 
        public virtual PMOCatsRequestInfo Copy(PMOCatsRequestInfo src, bool copyId = false)
        {
            var dst = this;

            dst.cats = src.cats;

            return base.Copy(src, copyId);
        }
    }
    [Table("PMODMSInfo")]
    public class PMODMSRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "PMODMS" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        [Display(Name = "DMS")]
        public bool dms { get; set; }

        public virtual PMODMSRequestInfo Copy(PMODMSRequestInfo src, bool copyId = false)
        {
            var dst = this;

            dst.dms = src.dms;

            return base.Copy(src, copyId);
        }
    }
    [Table("PMONPDIInfo")]
    public class PMONPDIRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "PMONPDI" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        [Display(Name = "NPDI")]
        public bool npdi { get; set; }

        public virtual PMONPDIRequestInfo Copy(PMONPDIRequestInfo src, bool copyId = false)
        {
            var dst = this;

            dst.npdi = src.npdi;

            return base.Copy(src, copyId);
        }
    }
    [Table("PMONESTMSInfo")]
    public class PMONESTMSRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "PMONESTMS" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        [Display(Name = "NESTMS")]
        public bool nestms { get; set; }

        public virtual PMONESTMSRequestInfo Copy(PMONESTMSRequestInfo src, bool copyId = false)
        {
            var dst = this;

            dst.nestms = src.nestms;

            return base.Copy(src, copyId);
        }
    }
    [Table("MailCaseRequestInfo")]
    public class MailCaseRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Mail case" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requestd by Form" : base.StaticDescription;
            }
        }
    }
    [Table("ClothesRequestInfo")]
    public class ClothesRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Work clothes" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        [Display(Name = "Security shoes")]
        public bool secuShoes { get; set; }
        public double shoeSize { get; set; }
        [Display(Name = "Gloves")]
        public bool gloves { get; set; }
        public string glovesSize { get; set; }
        [Display(Name = "Winter coat")]
        public bool winterCoat { get; set; }
        public string winterCoatSize { get; set; }
        [Display(Name = "Polar")]
        public bool polar { get; set; }
        public string polarSize { get; set; }
        [Display(Name = "Bardusch Clothes")]
        public bool bardushClothes { get; set; }
        public string bardusClothesSize { get; set; }
        public virtual ClothesRequestInfo Copy(ClothesRequestInfo src, bool copyId = false)
        {
            var dst = this;
            dst.secuShoes = src.secuShoes;
            dst.shoeSize = src.shoeSize;
            dst.gloves = src.gloves;
            dst.glovesSize = src.glovesSize;
            dst.winterCoat = src.winterCoat;
            dst.winterCoatSize = src.winterCoatSize;
            dst.polar = src.polar;
            dst.polarSize = src.polarSize;
            dst.bardushClothes = src.bardushClothes;
            dst.bardusClothesSize = src.bardusClothesSize;
            return base.Copy(src, copyId);
        }
    }
    [Table("LockerRequestInfo")]
    public class LockerRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Locker" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
    }
    [Table("IntroRequestInfo")]
    public class IntroRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Invitation Introduction" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Requested by Form" : base.StaticDescription;
            }
        }
        public string introLanguage { get; set; }
        public virtual IntroRequestInfo Copy(IntroRequestInfo src, bool copyId = false)
        {
            var dst = this;
            dst.introLanguage = src.introLanguage;
            return base.Copy(src, copyId);
        }
    }
    [Table("ExternalyManagedRequestInfo")]
    public class ExternalyManagedRequestInfo : AbstractRequestInfo
    {
        
        public virtual string Subjections { get; set; }
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Individualised introduction program" : base.StaticName;
            }
            set
            {
                base.StaticName = value;
            }
        }
        public override string StaticDescription
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Template intro program" : base.StaticDescription;
            }
        }
        public virtual ExternalyManagedRequestInfo Copy(ExternalyManagedRequestInfo src, bool copyId = false)
        {
            var dst = this;
            dst.Subjections = src.Subjections;
            return base.Copy(src, copyId);
        }
    }

    public enum StatusRequestInfoEnum
    {
        Unecessary = 0, Completed, Todo
    }




    //public class AbstractFormulaireModel<T>
    //    where T : AbstractNewRequestModel
    //{
    //    [Key()]
    //    public long? Id { get; set; }
    //    public T RequestModel { get; set; }
    //    public DateTime dateCompleted { get; set; }
    //}

    public abstract class AbstractActionFormulaireModel<T> : AbstractFormulaireModel<T>
        where T : AbstractNewRequestModel
    {
        public ITRequestInfo ITInfo { get; set; }
        public TelephoneRequestInfo TelephoneInfo { get; set; }
        public RoleSAPRequestInfo RoleSAPInfo { get; set; }
        public PMORequestInfo PMOInfo { get; set; }
        public PMOCatsRequestInfo PMOCatsInfo { get; set; }
        public PMODMSRequestInfo PMODMSInfo { get; set; }
        public PMONPDIRequestInfo PMONPDIInfo { get; set; }
        public PMONESTMSRequestInfo PMONESTMSInfo { get; set; }
        public MailCaseRequestInfo MailCaseInfo { get; set; }
        public ClothesRequestInfo ClothesInfo { get; set; }
        public LockerRequestInfo LockerInfo { get; set; }
        public IntroRequestInfo IntroInfo { get; set; }
        public List<ExternalyManagedRequestInfo> ExternalyManagedInfoList { get; set; }


        public void DepartureInitializeExternalyManagedInfoList()
        {
            if (ExternalyManagedInfoList == null)
                ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();

            //Edit départ
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Give back work clothes", Subjections = @"Work clothes to give back to Marianne Emery (4320)" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Tidy up Office", Subjections = @"Let your workplace clean and empty the drawers and wardrobes" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Activate Out of office", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Update department presetation", Subjections = @"Update intranet, the Nest" });
        }
        public void BeforeInCommingInitializeExternalyManagedInfoList()
        {
            if (ExternalyManagedInfoList == null)
                ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();

            //Edit Arrivée transferts
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "N: Access", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Personalized introduction program",
                Subjections = @"<a href='http://teamroom.eur.nestle.com/RD_DMS/AssistantsPTCOrbe/Personal%20Newcomers/Arriv%C3%A9e%20nouveaux%20collaborateurs%20-%20Dossier%20de%20pr%C3%A9paration/Intro%20programme%202012.xls'>Template intro program</a>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Arrival announcement",
                Subjections = @"<a href='http://teamroom.eur.nestle.com/RD_DMS/AssistantsPTCOrbe/Personal%20Newcomers/Arriv%C3%A9e%20nouveaux%20collaborateurs%20-%20Dossier%20de%20pr%C3%A9paration/Template%20Arrival%20Note%20(ver.%20MAR2011).ppt'>Template Annonce Newcomers</a>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "First meal organisation",
                Subjections = "reservation + inviter DH ou Groupe leader"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "SHE documents preparation:", Subjections = @" " });

        }
        public void AfterInCommingInitializeExternalyManagedInfoList()
        {
            if (ExternalyManagedInfoList == null)
                ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();

            //Edit Arrivée transferts check-list part
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired=true, Name = "Receive at reception", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "SHE Instructions", Subjections = @" link on video" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Presentation", Subjections = @"Presentations to department's Head and to work collegues" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Precision on functions", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Badge creation at Securitas Room", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Find equipment", Subjections = @"Work shoes, work clothes" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Program presentation", Subjections = @"Present the introduction program to the newcomer" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Explain organisation", Subjections = @"Explain departments organigram, of PTC and organisation of Business Streams" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {IsRequired = true,
                Name = "Administratives instructions",
                Subjections = @"
               <pre>
                <ul>
                <li>Signatures </li>
                <li>Traveling account creation</li>
                <li>Credit card</li>
                <li>Reimbursement</li> 
                </ul>
                <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Info: Department's life",
                Subjections = @" 
               <pre>
                <ul>
                <li>Work Schedule</li>
                <li>badging</li>
                <li>flextime</li> 
                <li>We say 'Bonjour'</li>
                <li>Breaks</li>
                <li>Confidentiality (SOP)</li>
                </ul>
                <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Project management's informations",
                Subjections = @"
            <pre>
                <ul>
                <li>DMS</li>
                <li>NPDI</<li>
                <li>Hydra</li>
                <li>CATS</li>
                </ul> 
            <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Services's usage",

                Subjections = @"
                <pre>
                <ul>
                <li>Intranet</li>
                <li>Mail</li>
                <li>Telephone</li>
                <li>E-mail</li>
                <li>Fax</li>
                <li>Business card</li>
                <li>Travel request</li>
                <li>Car location</li>
                <li>Easy Mail</li>
                <li>Data confidentiality</li>
                </ul>
                <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Work instructions",
                Subjections = @"
                <pre>
                <ul> 
                <li>reporting</li>
                <li>deviation phone</li>
                <li>Outlook Usage(GAL, PST, OOO,etc.)</li>
                </ul>
                <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Practical tips",
                Subjections = @" 
            <pre>
            <ul>
                <li>Buildings architecture, logistics</li>
                <li>changing room</li>
                <li>cafeteria</li>
                <li>restaurant</li>
                <li>CAN</li>
                <li>Assistant</li>
            </ul>
            <pre>"
            });
            //<pre>
            //n'oubliez pas de :
            //<ul>
            //<li>changer la voiture</li>
            //<li><a href='http://mysomelinksite.org/'>clicker sur ce lien</a></li>
            //<li>acheter un chien</li>
            //<li>donner a manger au <b>chat<b></li>
            //</ul>
            //<pre>"

        }


        public IEnumerable<AbstractRequestInfo> RequestInfoEnumerator()
        {
            var items = new AbstractRequestInfo[]{
				ITInfo,
				TelephoneInfo,
				RoleSAPInfo,
				PMOInfo,
                PMOCatsInfo,
                PMODMSInfo,
                PMONPDIInfo,
                PMONESTMSInfo,
				MailCaseInfo,
                ClothesInfo,
				LockerInfo,
			};
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                    continue;

                yield return items[i];
            }
        }
    }

    public class AbstractCompleterFormulaireModel<T> : AbstractActionFormulaireModel<T>
        where T : AbstractNewRequestModel
    {
    }
    public class ArrivalCompleterFormulaireModel : AbstractCompleterFormulaireModel<ArrivalNewRequestModel>
    {
    }
    public class TransfertCompleterFormulaireModel : AbstractCompleterFormulaireModel<TransfertNewRequestModel>
    {
    }
    public class DepartureCompleterFormulaireModel : AbstractCompleterFormulaireModel<DepartureNewRequestModel>
    {
    }

    public class AbstractEditFormulaireModel<T> : AbstractActionFormulaireModel<T>
        where T : AbstractNewRequestModel
    {
        public IQueryable<Request> RequestSet { get; set; }
        public IQueryable<Request> CompletionRequestSet { get { return (from t in RequestSet where t.Id > 0 select t); } }
        public IQueryable<Request> AdministrationRequestSet { get { return (from t in RequestSet where t.Id > 0 select t); } }
        public IQueryable<Request> ArrivalRequestSet { get { return (from t in RequestSet where t.Kind == RequestKindEnum.Arrival select t); } }
        public IQueryable<Request> DepartureRequestSet { get { return (from t in RequestSet where t.Kind == RequestKindEnum.Departure select t); } }
        public IQueryable<Request> TransfertRequestSet { get { return (from t in RequestSet where t.Kind == RequestKindEnum.Transfert select t); } }

    }
    public class ArrivalEditFormulaireModel : AbstractEditFormulaireModel<ArrivalNewRequestModel>
    {
    }
    public class TransfertEditFormulaireModel : AbstractEditFormulaireModel<TransfertNewRequestModel>
    {
    }
    public class DepartureEditFormulaireModel : AbstractEditFormulaireModel<DepartureNewRequestModel>
    {
    }

}