using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nespe.Models
{
    public class AbstractNewRequestModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Veuillez entrer le nom")]
        [Display(Name = "Prénom")]
        public string SurnameNC { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le prénom")]
        [Display(Name = "Nom")]
        public string NameNC { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la date d'arrivée du nouveau collaborateur")]
        [Display(Name = "Date d'arrivée")]
        public DateTime StartDateNC { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un département")]
        [Display(Name = "Département")]
        public string DepartmentNC { get; set; }

        public RequestKindEnum kind { get; set; }

        public virtual Request Copy(Request model)
        {
            var r = this;
            r.kind = model.kind;
            r.SurnameNC = model.SurnameNC;
            r.NameNC = model.NameNC;
            r.StartDateNC = model.StartDateNC;
            r.Id = model.Id;
            r.SurnameNC = model.SurnameNC;
            r.NameNC = model.NameNC;
            r.StartDateNC = model.StartDateNC;
            r.DepartmentNC = model.DepartmentNC;

            return model;
        }
        public static implicit operator Request(AbstractNewRequestModel model)
        {
            var r = new Request
            {
                kind = model.kind,
                SurnameNC = model.SurnameNC,
                NameNC = model.NameNC,
                StartDateNC = model.StartDateNC,
            };
            r.Id = model.Id;
            r.SurnameNC = model.SurnameNC;
            r.NameNC = model.NameNC;
            r.StartDateNC = model.StartDateNC;
            r.DepartmentNC = model.DepartmentNC;
            return r;
        }


     
    }
    public class DepartureNewRequestModel : AbstractNewRequestModel
    {
        [Required(ErrorMessage = "Veuillez entrer la date d'arrivée du nouveau collaborateur")]
        public DateTime DepartureDate { get; set; }
        [Required(ErrorMessage = "Veuillez entrer le local")]
        public string Local { get; set; }

        public bool retirement { get; set; }

        public override Request Copy(Request model)
        {
            var r = this;

            r.Local = model.LocalNC;
            r.Id = model.Id;
            r.SurnameNC = model.SurnameNC;
            r.NameNC = model.NameNC;
            r.StartDateNC = model.StartDateNC;
            r.DepartmentNC = model.DepartmentNC;
            return base.Copy(model);
        }
        public static implicit operator Request(DepartureNewRequestModel model){
            var o = model as AbstractNewRequestModel;
            Request r = o;
            r.LocalNC = model.Local;
            r.Id = model.Id;
            r.SurnameNC = model.SurnameNC;
            r.NameNC = model.NameNC;
            r.StartDateNC = model.StartDateNC;
            r.DepartmentNC = model.DepartmentNC;
            return r;
        }

        public static implicit operator DepartureNewRequestModel(Request model)
        {

            var r = new DepartureNewRequestModel();
            r.Copy(model);
            return r;
        }
    }
    public class ArrivalNewRequestModel : AbstractNewRequestModel
    {

        [Display(Name = "Fonction")]
        public string FunctionNC { get; set; }

        [Display(Name = "Supérieur hierarchique")]
        public string SuperiorNC { get; set; }

        [Display(Name = "Business Stream")]
        public string BusinessStreamNC { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le numéro d'employé, s'il n'a pas de compte SAP, cochez la case 'Non-SAP'")]
        [Display(Name = "Numéro d'employé")]
        public string EmployeeNumberNC { get; set; }

        public bool nonSAPNC { get; set; }

        [Display(Name = "Local")]
        public string LocalNC { get; set; }

        [Display(Name = "Téléphone")]
        public string PhoneNC { get; set; }

        [Display(Name = "Initiales")]
        public string initialsNC { get; set; }

        [Display(Name = "Parrain")]
        public string Parrain { get; set; }

        public override Request Copy(Request model) {
            var r = this;
            
            r.FunctionNC = model.FunctionNC;
            r.LocalNC = model.LocalNC;
            r.FunctionNC = model.FunctionNC;
            r.Id = model.Id;
            r.SurnameNC = model.SurnameNC;
            r.NameNC = model.NameNC;
            r.DepartmentNC = model.DepartmentNC;
            r.FunctionNC = model.FunctionNC;
            r.SuperiorNC = model.SuperiorNC;
            r.BusinessStreamNC = model.BusinessStreamNC;
            r.StartDateNC = model.StartDateNC;
            r.EmployeeNumberNC = model.EmployeeNumberNC;
            r.nonSAPNC = model.nonSAPNC;
            r.LocalNC = model.LocalNC;
            r.PhoneNC = model.PhoneNC;
            r.initialsNC = model.initialsNC;
            //r.TransFrom = model.TransFrom;
            //r.Kind = (short)model.kind;
            r.Parrain = model.Parrain;
            return base.Copy(model);
        }
        public static implicit operator Request(ArrivalNewRequestModel model)
        {
            var o = model as AbstractNewRequestModel;
            Request r = o;
            r.FunctionNC = model.FunctionNC;
            r.LocalNC = model.LocalNC;
            r.FunctionNC = model.FunctionNC;
            r.Id = model.Id;
            r.SurnameNC = model.SurnameNC;
            r.NameNC = model.NameNC;
            r.DepartmentNC = model.DepartmentNC;
            r.FunctionNC = model.FunctionNC;
            r.SuperiorNC = model.SuperiorNC;
            r.BusinessStreamNC = model.BusinessStreamNC;
            r.StartDateNC = model.StartDateNC;
            r.EmployeeNumberNC = model.EmployeeNumberNC;
            r.nonSAPNC = model.nonSAPNC;
            r.LocalNC = model.LocalNC;
            r.PhoneNC = model.PhoneNC;
            r.initialsNC = model.initialsNC;
            //r.TransFrom = model.TransFrom;
            //r.Kind = (short)model.kind;
            r.Parrain = model.Parrain;
            return r;
        }

        public static implicit operator ArrivalNewRequestModel(Request model)
        {
            
            var  r =new ArrivalNewRequestModel();
            r.Copy(model);
            return r;
        }
        
    }
    public class TransfertNewRequestModel : ArrivalNewRequestModel
    {
        [Display(Name = "Transféré de")]
        public string TransFrom { get; set; }


        public override Request Copy(Request model)
        {
            var r = this;

            r.TransFrom = model.TransFrom;
            return base.Copy(model);
        }
        public static implicit operator Request(TransfertNewRequestModel model)
        {
            var o = model as ArrivalNewRequestModel;
            Request r = o;
            r.TransFrom = model.TransFrom;
            return r;
        }

        public static implicit operator TransfertNewRequestModel(Request model)
        {

            var r = new TransfertNewRequestModel();
            r.Copy(model);
            return r;
        }
    }


   
}