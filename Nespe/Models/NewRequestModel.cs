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
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le prénom")]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la date d'arrivée du nouveau collaborateur")]
        [Display(Name = "Date d'arrivée")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un département")]
        [Display(Name = "Département")]
        public long Department_Id { get; set; }

        public RequestKindEnum kind { get; set; }

        public virtual Request Copy(Request src)
        {
            var dst = this;
            dst.kind = src.Kind;
            dst.StartDate = src.StartDate;
            dst.Id = src.Id;
            dst.StartDate = src.StartDate;
            Copy(src.PersonDepartment);
            return src;
        }
        public virtual PersonDepartment Copy(PersonDepartment src)
        {
            var dst = this;
            Copy(src.Department);
            Copy(src.Person);
            return src;
        }
        public virtual Department Copy(Department src)
        {
            var dst = this;
            dst.Department_Id = src.Id;
            return src;
        }
        public virtual Person Copy(Person src)
        {
            var dst = this;
            dst.FirstName = src.FirstName;
            dst.LastName = src.LastName;
            return src;
        }
        public virtual Request CopyTo(Request dst)
        {
            var src = this;
            dst.Id = src.Department_Id;
            CopyTo(dst.PersonDepartment);
            return dst;
        }
        public virtual PersonDepartment CopyTo(PersonDepartment dst)
        {
            var src = this;
            CopyTo(dst.Department);
            CopyTo(dst.Person);
            return dst;
        }
        public virtual Department CopyTo(Department dst)
        {
            var src = this;
            dst.Id = src.Department_Id;
            return dst;
        }
        public virtual Person CopyTo(Person dst)
        {
            var src = this;
            dst.FirstName = src.FirstName;
            dst.LastName = src.LastName;
            return dst;
        }
        public static implicit operator Request(AbstractNewRequestModel src)
        {
            var dst = new Request
            {
                Kind = src.kind,
                
            };
            dst.Id = src.Id;
            src.CopyTo(dst);
            return dst;
        }


     
    }
    public class DepartureNewRequestModel : AbstractNewRequestModel
    {
        [Required(ErrorMessage = "Veuillez entrer la date d'arrivée du nouveau collaborateur")]
        public DateTime DepartureDate { get; set; }
        [Required(ErrorMessage = "Veuillez entrer le local")]
        public string Local { get; set; }

        public bool retirement { get; set; }

        public override Request Copy(Request src)
        {
            var dst = this;

            dst.Local = src.Local;
            return base.Copy(src);
        }
        public override Request CopyTo(Request dst)
        {
            var src = this;

            dst.Local = src.Local;
            return base.CopyTo(dst);
        }
        public static implicit operator Request(DepartureNewRequestModel src)
        {
            var o = src as AbstractNewRequestModel;
            Request dst = o;
            src.CopyTo(dst);
            return dst;
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
        public string Function { get; set; }

        [Display(Name = "Supérieur hierarchique")]
        public string SuperiorNC { get; set; }

        [Display(Name = "Business Stream")]
        public string BusinessStreamNC { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le numéro d'employé, s'il n'a pas de compte SAP, cochez la case 'Non-SAP'")]
        [Display(Name = "Numéro d'employé")]
        public string EmployeeNumberNC { get; set; }

        public bool nonSAPNC { get; set; }

        [Display(Name = "Local")]
        public string Local { get; set; }

        [Display(Name = "Téléphone")]
        public string Phone { get; set; }

        [Display(Name = "Initiales")]
        public string Initials { get; set; }

        [Display(Name = "Parrain")]
        public string Parrain { get; set; }


        public override Request Copy(Request src) {
            var dst = this;
            
            dst.Local = src.Local;
            dst.Id = src.Id;
            dst.SuperiorNC = src.Superior;
            dst.BusinessStreamNC = src.BusinessStream;
            dst.StartDate = src.StartDate;
            dst.EmployeeNumberNC = src.EmployeeNumber;
            dst.nonSAPNC = src.nonSAP;
            dst.Local = src.Local;
            //dst.TransFrom = dst.TransFrom;
            //dst.Kind = (short)dst.Kind;
            dst.Parrain = src.Parrain;
            return base.Copy(src);
        }
        public override Person Copy(Person src)
        {
            var dst = this;
            dst.Phone = src.Phone;
            dst.Initials = src.Initials;
            return base.Copy(src);
        }
        public static implicit operator Request(ArrivalNewRequestModel src)
        {
            var o = src as AbstractNewRequestModel;
            Request dst = o;
            //dst.FunctionNC = src.Function;
            //dst.LocalNC = src.Local;
            //dst.FunctionNC = src.Function;
            //dst.Id = src.Id;
            //dst.SurnameNC = src.FirstName;
            //dst.NameNC = src.LastName;
            //dst.DepartmentNC = src.Department_Id;
            //dst.FunctionNC = src.Function;
            //dst.SuperiorNC = src.SuperiorNC;
            //dst.BusinessStreamNC = src.BusinessStreamNC;
            //dst.StartDateNC = src.StartDate;
            //dst.EmployeeNumberNC = src.EmployeeNumberNC;
            //dst.nonSAPNC = src.nonSAPNC;
            //dst.LocalNC = src.Local;
            //dst.PhoneNC = src.Phone;
            //dst.initialsNC = src.Initials;
            //dst.TransFrom = dst.TransFrom;
            //dst.Kind = (short)dst.Kind;
            dst.Parrain = src.Parrain;
            return dst;
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