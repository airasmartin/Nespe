using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Linq;
//using System.Data.Linq.Mapping;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Nespe.Models;


namespace Nespe.Models
{
    [Table("tbl_request")]
    public class Request
    { 
        public Request()
        {
            StateMachine = new StateMachine(this);
        }
        [Key, Column]
        public long Id { get; set; }
        [Column]
        public long? PersonDepartment_Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public StateMachine StateMachine { get; private set; }


        [Display(Name = "PersonDepartment"), ForeignKey("PersonDepartment_Id")]
        public PersonDepartment PersonDepartment { get; set; }
        private PersonDepartment _PersonDepartment() { return PersonDepartment != null ? PersonDepartment : PersonDepartment = new PersonDepartment { }; }
        [Display(Name = "Person"), NotMapped]
        public Person Person { get { return _PersonDepartment().Person; } set { _PersonDepartment().Person = value; } }
        [Display(Name = "Department_Id"), NotMapped]
        public Department Department { get { return _PersonDepartment().Department; } set { _PersonDepartment().Department = value; } }


        [Display(Name = "Fonction")]
        public string Function { get; set; }
        
        public string Superior { get; set; }
        
        public string BusinessStream { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la date d'arrivée du nouveau collaborateur")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le numéro d'employé, s'il n'a pas de compte SAP, cochez la case 'Non-SAP'")]
        public string EmployeeNumber { get; set; }
        
        [Display(Name="A un code SAP")]
        public bool nonSAP { get; set; }
        
        public string Local { get; set; }
        
        public string TransFrom { get; set; }

        public RequestKindEnum Kind { get; set; }

        public string Parrain { get; set; }

        public bool Completed { get; set; }



        [Required(AllowEmptyStrings=true), Display(Name = "Active Directory Id")]
        public string ActiveDirectoryId { get; set; }

        public bool HaveActiveDirectoryId { get { return !string.IsNullOrWhiteSpace(ActiveDirectoryId); } }

        // Jour depassé ?
        public TimeSpan ElapsedTime { get { return DateTime.Now.Subtract(StartDate); } }
        // Jour depassé ?
        public bool IsOverTime { get { return ElapsedTime.Ticks>0; } }
        // Jour disponibles ?
        public TimeSpan RemainingTime { get { return StartDate.Subtract(DateTime.Now); } }

      









        //public static implicit operator RequestEntity(Request dst)
        //{
        //    RequestEntity dst = null;
        //    using (var db = new NespeEntityContainer()) {
        //        dst = (from t in db.Requests where t.Id == dst.Id select t).FirstOrDefault();
        //        if (dst == null) {
        //            dst = db.Requests.CreateObject();
        //        }
        //    }
        //    //public:b+{[^:b]+}:b+{[^:b]+}:b+.+
        //    //dst.\2=dst.\2;
        //    dst.Id = dst.Id;
        //    dst.FirstName = dst.FirstName;
        //    dst.LastName = dst.LastName;
        //    dst.Department_Id = dst.Department_Id;
        //    dst.Function = dst.Function;
        //    dst.SuperiorNC = dst.SuperiorNC;
        //    dst.BusinessStreamNC = dst.BusinessStreamNC;
        //    dst.StartDate = dst.StartDate;
        //    dst.EmployeeNumberNC = dst.EmployeeNumberNC;
        //    dst.nonSAPNC = dst.nonSAPNC;
        //    dst.Local = dst.Local;
        //    dst.Phone = dst.Phone;
        //    dst.Initials = dst.Initials;
        //    dst.TransFrom = dst.TransFrom;
        //    dst.Kind = (short)dst.Kind;
        //    dst.Parrain = dst.Parrain;

            
        //    return dst;
        //}
        //public static implicit operator Request(RequestEntity dst)
        //{
        //    Request dst = new Request();
        //    dst.Id = dst.Id;
        //    dst.FirstName = dst.FirstName;
        //    dst.LastName = dst.LastName;
        //    dst.Department_Id = dst.Department_Id;
        //    dst.Function = dst.Function;
        //    dst.SuperiorNC = dst.SuperiorNC;
        //    dst.BusinessStreamNC = dst.BusinessStreamNC;
        //    dst.StartDate = dst.StartDate == null ? DateTime.Now : dst.StartDate.Value;
        //    dst.EmployeeNumberNC = dst.EmployeeNumberNC;
        //    dst.nonSAPNC = dst.nonSAPNC == null ? true : dst.nonSAPNC.Value;
        //    dst.Local = dst.Local;
        //    dst.Phone = dst.Phone;
        //    dst.Initials = dst.Initials;
        //    dst.TransFrom = dst.TransFrom;
        //    dst.Kind = (RequestKindEnum)(dst.Kind == null ? 0 : dst.Kind);
        //    dst.Parrain = dst.Parrain;


        //    return dst;
        //}
    }

    public enum RequestKindEnum {
        [Display(Name = "nouveau arrivé")]
        Arrival=0,
        [Display(Name = "Transfert de ")]
        Transfert = 1,
        [Display(Name = "Bye bye")]
        Departure=2
    }
    public enum RequestStatusEnum
    {
        [Display(Name = "nouveau arrivé")]
        Initier = 0,
        [Display(Name = "Transfert de ")]
        Completer = 1,
        [Display(Name = "Bye bye")]
        Arrival = 2
    }
}