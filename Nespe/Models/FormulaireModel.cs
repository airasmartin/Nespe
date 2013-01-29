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
        [Display(Name = "Commentaires:")]
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
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Matériel IT" : base.StaticName;
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
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Demadé par le formulaire" : base.StaticDescription;
            }
        }
        [Display(Name = "Laptop")]
        public bool Laptop { get; set; }
        [Display(Name = "Docking Station")]
        public bool DockingStation { get; set; }
        [Display(Name = "Clavier")]
        public bool Keyboard { get; set; }
        [Display(Name = "Desktop")]
        public bool Desktop { get; set; }
        [Display(Name = "Ecran")]
        public bool Screen { get; set; }
        [Display(Name = "Souris")]
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
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Téléphone" : base.StaticName;
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
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Demandé par le formulaire" : base.StaticDescription;
            }
        }
        public bool fixPhone { get; set; }
        [Display(Name = "Cordless")]
        public bool cordless { get; set; }
        [Display(Name = "Mobile")]
        public bool mobile { get; set; }
        [Display(Name = "Smartphone")]
        public bool smartphone { get; set; }
        [Display(Name = "Casque pour téléphone fixe")]
        public bool headphoneForFix { get; set; }
        [Display(Name = "Casque pour Cordless")]
        public bool headphoneForCordless { get; set; }
        [Display(Name = "Kit mains libres pour fixe")]
        public bool freeHandsForFix { get; set; }
        [Display(Name = "Kit mains libres pour Cordless")]
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
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Rôles SAP" : base.StaticName;
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
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Demandé par le formulaire" : base.StaticDescription;
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
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Demandé par le formulaire" : base.StaticDescription;
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
    [Table("MailCaseRequestInfo")]
    public class MailCaseRequestInfo : AbstractRequestInfo
    {
        public override string StaticName
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Case courrier" : base.StaticName;
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
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Demandé par le formulaire" : base.StaticDescription;
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
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Habits de travail" : base.StaticName;
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
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Demandé par le formulaire" : base.StaticDescription;
            }
        }
        [Display(Name = "Chaussures de sécurité")]
        public bool secuShoes { get; set; }
        public double shoeSize { get; set; }
        [Display(Name = "Gants")]
        public bool gloves { get; set; }
        public string glovesSize { get; set; }
        [Display(Name = "Veste d'hiver")]
        public bool winterCoat { get; set; }
        public string winterCoatSize { get; set; }
        [Display(Name = "Polaire")]
        public bool polar { get; set; }
        public string polarSize { get; set; }
        [Display(Name = "Vêtements de travail Bardusch")]
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
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Vestiaire, casier" : base.StaticName;
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
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Demandé par le formulaire" : base.StaticDescription;
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
                return string.IsNullOrWhiteSpace(base.StaticDescription) ? "Demandé par le formulaire" : base.StaticDescription;
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
                return string.IsNullOrWhiteSpace(base.StaticName) ? "Programme Intro individualisé" : base.StaticName;
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
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Rendre habits de travail", Subjections = @"Habits à rentre à Marianne Emery (4320)" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Rangement Bureau", Subjections = @"Laisse ta place de travail propre et vide tes tiroirs et armoires" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Activation Out of office", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Mise à jour presentation dépt", Subjections = @"mettre à jour l'intranet The Nest" });
        }
        public void BeforeInCommingInitializeExternalyManagedInfoList()
        {
            if (ExternalyManagedInfoList == null)
                ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();

            //Edit Arrivée transferts
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Accès au N:", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Programme Intro individualisé",
                Subjections = @"<a href='http://teamroom.eur.nestle.com/RD_DMS/AssistantsPTCOrbe/Personal%20Newcomers/Arriv%C3%A9e%20nouveaux%20collaborateurs%20-%20Dossier%20de%20pr%C3%A9paration/Intro%20programme%202012.xls'>Template intro program</a>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Annonce d'arrivée",
                Subjections = @"<a href='http://teamroom.eur.nestle.com/RD_DMS/AssistantsPTCOrbe/Personal%20Newcomers/Arriv%C3%A9e%20nouveaux%20collaborateurs%20-%20Dossier%20de%20pr%C3%A9paration/Template%20Arrival%20Note%20(ver.%20MAR2011).ppt'>Template Annonce Newcomers</a>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Organisation 1er repas",
                Subjections = @"<a href='http://thenest-eur-hq.nestle.com/RD/RD_RLOC/Europe/OrbePTC/documents/indexable/Facility%20Management/2009-11%20Process%20Complaints%20V2-091101.pdf'>réservation</a> + inviter DH ou Groupe leader"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Préparation documents SHE:", Subjections = @" " });

        }
        public void AfterInCommingInitializeExternalyManagedInfoList()
        {
            if (ExternalyManagedInfoList == null)
                ExternalyManagedInfoList = new List<ExternalyManagedRequestInfo>();

            //Edit Arrivée transferts check-list part
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired=true, Name = "Accueil à la réception", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Instructions SHE", Subjections = @" liens sur vidéo" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Présentation", Subjections = @"Présentation au chef de département et aux collègues de travail" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Précision sur les fonctions", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Loge sécurité pour établir le badge", Subjections = @" " });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Chercher équipement", Subjections = @"Chaussures de sécurité, habits de travail, EPI" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Présentation programme", Subjections = @"Présenter le programme d'introduction au nouveau collaborateur" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo { IsRequired = true, Name = "Explication organisation", Subjections = @"Expliquer l'organigramme du département, du PTC, l'organisation des Business Streams" });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {IsRequired = true,
                Name = "Instructions Administratives",
                Subjections = @"
               <pre>
                <ul>
                <li>Signatures </li>
                <li>Création compte voyageur</li>
                <li>Carte de crédit</li>
                <li>Remboursement frais</li> 
                </ul>
                <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Info: la vie du département",
                Subjections = @" 
               <pre>
                <ul>
                <li>Horarie de travail</li>
                <li>badgeage</li>
                <li>flextime</li> 
                <li>on dit Bonjour</li>
                <li>Pauses</li>
                <li>Confidentialité (SOP)</li>
                </ul>
                <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Informations de base relatives à la gestion de projet",
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
                Name = "Utilisation des services",

                Subjections = @"
                <pre>
                <ul>
                <li>Intranet</li>
                <li>Courrier</li>
                <li>Téléphone</li>
                <li>E-mail</li>
                <li>Fax</li>
                <li>Cartes de visite</li>
                <li>Demande de voyage</li>
                <li>Location de voiture</li>
                <li>Easy Mail</li>
                <li>Confidentialité des données</li>
                </ul>
                <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Instructions de travail",
                Subjections = @"
                <pre>
                <ul> 
                <li>établissement des rapports</li>
                <li>déviation téléphonique</li>
                <li>utilisation Outlook (GAL, PST, OOO,etc.)</li>
                </ul>
                <pre>"
            });
            ExternalyManagedInfoList.Add(new ExternalyManagedRequestInfo
            {
                IsRequired = true,
                Name = "Conseils pratiques",
                Subjections = @" 
            <pre>
            <ul>
                <li>architecture bâtiments, logistique</li>
                <li>vestiaires</li>
                <li>cafétéria</li>
                <li>restaurant</li>
                <li>CAN</li>
                <li>tourniquet</li>
                <li>Assistante</li>
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