using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Web;
using System.ComponentModel.DataAnnotations;
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
   
    public class RequestTypeInfo
    {
        [Key(), Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public long Id { get; set; }
        [Column(Name="Request_Id")]
        public long? RequestId { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        //public static implicit operator Nespe.RequestInfoTypeEntity(RequestTypeInfo v){
        //    var dst = new Nespe.RequestInfoTypeEntity { };
        //    dst.Id = v.Id;
        //    dst.Name = v.Name;
        //    dst.Description = v.Description;
        //    return dst;
        //}

    }
    public abstract class AbstractRequestInfo
    {
        [Key()]
        public long Id { get; set; }
        public long? Request_Id { get; set; }
        public string Name { get; set; }
        public string Executor { get; set; }
        public StatusRequestInfoEnum Status { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

    }
    public class ITRequestInfo : AbstractRequestInfo
    {
        public bool necessaryIT { get; set; }
        [Display(Name = "Laptop")]
        public bool laptop { get; set; }
        [Display(Name = "Docking Station")]
        public bool docking { get; set; }
        [Display(Name = "Clavier")]
        public bool keyboard { get; set; }
        [Display(Name = "Desktop")]
        public bool desktop { get; set; }
        [Display(Name = "Ecran")]
        public bool screen { get; set; }
        [Display(Name = "Souris")]
        public bool mouse { get; set; }
        [Display(Name = "Commentaires IT:")]
        public string commentIT { get; set; }

    }
    public class TelephoneRequestInfo : AbstractRequestInfo
    {
        public bool necessaryPhone { get; set; }
        [Display(Name = "Téléphone fixe")]
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
        [Display(Name = "Commentaires téléphones :")]
        public string commentPhone { get; set; }
    }
    public class RoleSAPRequestInfo : AbstractRequestInfo
    {
        public bool necessaryRole { get; set; }
        [Display(Name = "Purchase Requester")]
        public string purchaseType { get; set; }
        [Display(Name = "Purchase Approver")]
        public bool pCard { get; set; }
        [Display(Name = "Commentaires rôles SAP :")]
        public string commentSAP { get; set; }
    }
    public class PMORequestInfo : AbstractRequestInfo
    {
        public bool necessaryPMO { get; set; }
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
        [Display(Name = "Commentaires PMO :")]
        public string commentPMO { get; set; }
    }
    public class MailCaseRequestInfo : AbstractRequestInfo
    {
        public bool necessaryMailCase { get; set; }
        [Display(Name = "Commentaires Case courrier :")]
        public string commentMailCase { get; set; }
    }
    public class ClothesRequestInfo : AbstractRequestInfo
    {
        public bool necessarySecu { get; set; }
        [Display(Name = "Chaussures de sécurité")]
        public bool secuSheoes { get; set; }
        public double shoeSize { get; set; }
        [Display(Name = "Gants")]
        public bool gloves { get; set; }
        public double glovesSize { get; set; }
        [Display(Name = "Veste d'hiver")]
        public bool winterCoat { get; set; }
        public double winterCoatSize { get; set; }
        [Display(Name = "Polaire")]
        public bool polar { get; set; }
        public double polarSize { get; set; }
        [Display(Name = "Vêtements de travail Bardusch")]
        public bool bardushClothes { get; set; }
        public double bardusClothesSize { get; set; }
        [Display(Name = "Commentaire :")]
        public string commentSecu { get; set; }
    }

    public class LockerRequestInfo : AbstractRequestInfo
    {
        public bool necesaryLocker { get; set; }
        [Display(Name = "Commentaires casier vestiaire :")]
        public string commentLocker { get; set; }
    }
    public class IntroRequestInfo : AbstractRequestInfo
    {
        public bool necessaryIntro { get; set; }
        public string introLanguage { get; set; }
        [Display(Name = "Commentaires intro")]
        public string commentIntro { get; set; }
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


        public IEnumerable RequestInfoEnumerator()
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
	
}