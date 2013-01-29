using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Linq;
using System.ComponentModel.DataAnnotations;

namespace Nespe.Models
{
    public class AbstractNewRequestModel
    {
        
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Veuillez entrer le prénom", AllowEmptyStrings=true)]
        [Display(Name = "*Prénom")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le nom", AllowEmptyStrings = true)]
        [Display(Name = "*Nom")]
        public virtual string LastName { get; set; }


        //[Required(ErrorMessage = "Veuillez entrer l'Active Directory Id")]
        [Display(Name = "Active Directory Id")]
        public virtual string ActiveDirectoryId { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la date d'arrivée du nouveau collaborateur")]
        [Display(Name = "*Date d'arrivée")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un département")]
        [Display(Name = "*Département")]
        public virtual long Department_Id { get; set; }

        public virtual RequestKindEnum kind { get; set; }

        public virtual Request Copy(Request src)
        {
            if (src == null)
                return src;
            var dst = this;
            dst.ActiveDirectoryId = src.ActiveDirectoryId;
            dst.kind = src.Kind;
            dst.StartDate = src.StartDate;
            dst.Id = src.Id;
            dst.StartDate = src.StartDate;
            Copy(src.PersonDepartment);
            return src;
        }
        public virtual PersonDepartment Copy(PersonDepartment src)
        {
            if (src == null)
                return src;
            var dst = this;
            Copy(src.Department);
            Copy(src.Person);
            return src;
        }
        public virtual Department Copy(Department src)
        {
            if (src == null)
                return src;
            var dst = this;
            dst.Department_Id = src.Id;
            return src;
        }
        public virtual Person Copy(Person src)
        {
            if (src == null)
                return src;
            var dst = this;
            dst.FirstName = src.FirstName;
            dst.LastName = src.LastName;
            return src;
        }
        public virtual Request CopyTo(Request dst)
        {

            var src = this;
            dst.ActiveDirectoryId = src.ActiveDirectoryId;
            dst.Kind = src.kind;
            dst.StartDate = src.StartDate;
            dst.Id = src.Id;
            dst.StartDate = src.StartDate;
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
                PersonDepartment = new PersonDepartment { Person = new Person { }, Department = new Department { } },

            };
            dst.Id = src.Id;
            src.CopyTo(dst);
            return dst;
        }


     
    }
    public class DepartureNewRequestModel : AbstractNewRequestModel
    {
        [Display(Name = "Initiales")]
        public string Initials { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la date de départ du collaborateur")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get { return StartDate; } set { StartDate = value; } }
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
            dst.Kind = RequestKindEnum.Departure;
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
        public string Superior { get; set; }

        [Display(Name = "Business Stream")]
        public string BusinessStream { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le numéro d'employé, s'il n'a pas de compte SAP, cochez la case 'Non-SAP'")]
        [Display(Name = "*Numéro d'employé")]
        public string EmployeeNumber { get; set; }

        public bool nonSAP { get; set; }

        [Display(Name = "Local")]
        public string Local { get; set; }

        [Display(Name = "Téléphone")]
        public string Phone { get; set; }

        [Display(Name = "Initiales")]
        public string Initials { get; set; }

        [Display(Name = "Parrain")]
        public string Parrain { get; set; }



        public override Request CopyTo(Request dst)
        {
            var src = this;

            dst.Local = src.Local;
            dst.Id = src.Id;
            dst.Function = src.Function;
            dst.Superior = src.Superior;
            dst.BusinessStream = src.BusinessStream;
            dst.Kind = RequestKindEnum.Arrival;
            dst.EmployeeNumber = src.EmployeeNumber;

            dst.nonSAP = src.nonSAP;
            dst.Local = src.Local;
            if(dst.PersonDepartment==null)
                dst.PersonDepartment=new PersonDepartment{Person=new Person{}, Department=new Department{}};
            if (dst.Person == null)
                dst.Person = new Person { };
            if (dst.Department == null)
                dst.Department = new Department { };
            dst.Person.Phone = src.Phone;
            dst.Person.Initials = src.Initials;
            dst.Department.Id = src.Department_Id;
            dst.Parrain = src.Parrain;
            dst.StartDate = src.StartDate;


            return base.CopyTo(dst);
        }

        public override Request Copy(Request src) {
            if (src == null)
                return src;

            var dst = this;
            

            dst.Local = src.Local;
            dst.Id = src.Id;
            dst.Function = src.Function;
            dst.Superior = src.Superior;
            dst.BusinessStream = src.BusinessStream;

            dst.EmployeeNumber = src.EmployeeNumber;

            dst.nonSAP = src.nonSAP;
            if (src.Person != null)
            {
                dst.Phone = src.Person.Phone;
                dst.Initials = src.Person.Initials;
            }
            dst.Parrain = src.Parrain;
            dst.StartDate = src.StartDate;

            dst.Parrain = src.Parrain;
            return base.Copy(src);
        }
        public override Person Copy(Person src)
        {
            if (src == null)
                return src;
            var dst = this;
            dst.Phone = src.Phone;
            dst.Initials = src.Initials;
            return base.Copy(src);
        }
        public static implicit operator Request(ArrivalNewRequestModel src)
        {
            var o = src as AbstractNewRequestModel;
            Request dst = o;
            src.CopyTo(dst);
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


        public override Request Copy(Request src)
        {
            var dst = this;
            
            dst.TransFrom = src.TransFrom;
            return base.Copy(src);
        }
        public override Request CopyTo(Request dst)
        {
            var src = this;
            dst.Kind = RequestKindEnum.Transfert;
            dst.TransFrom = src.TransFrom;
            return base.CopyTo(dst);
        }
        public override Person CopyTo(Person dst)
        {
            
            var src = this;
            dst.Initials = src.Initials;
            dst.Phone = src.Phone;
            return base.CopyTo(dst);
        }

        public static implicit operator Request(TransfertNewRequestModel model)
        {
            var src = model as ArrivalNewRequestModel;
            Request dst = src;
            dst.TransFrom = model.TransFrom;
            return src.CopyTo(dst);
        }

        public static implicit operator TransfertNewRequestModel(Request src)
        {

            var dst = new TransfertNewRequestModel();
            dst.Copy(src);
            return dst;
        }
    }


   
}