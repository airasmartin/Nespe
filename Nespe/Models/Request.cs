using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;


namespace Nespe
{
    
    public class Request
    { 
        public Request()
        {
            StateMachine = new StateMachine(this);
        }
        [Key()]
        public int Id { get; set; }
        public StateMachine StateMachine { get; private set; }

        [Required(ErrorMessage="Veuillez entrer le nom")]
        [Display(Name="Prénom")]
        public string SurnameNC { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le prénom")]
        [Display(Name = "Nom")]
        public string NameNC { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un département")]
        [Display(Name = "Département")]
        public string DepartmentNC { get; set; }

        [Display(Name = "Fonction")]
        public string FunctionNC { get; set; }
        
        public string SuperiorNC { get; set; }
        
        public string BusinessStreamNC { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la date d'arrivée du nouveau collaborateur")]
        public DateTime StartDateNC { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le numéro d'employé, s'il n'a pas de compte SAP, cochez la case 'Non-SAP'")]
        public string EmployeeNumberNC { get; set; }
        
        public bool nonSAPNC { get; set; }
        
        public string LocalNC { get; set; }
        
        public string PhoneNC { get; set; }
        
        public string initialsNC { get; set; }
        
        public string TransFrom { get; set; }

        public RequestKindEnum kind { get; set; }

        public string Parrain { get; set; }

        public bool formCompleted { get; set; }

        // Jour depassé ?
        public TimeSpan ElapsedTime { get { return DateTime.Now.Subtract(StartDateNC); } }
        // Jour depassé ?
        public bool IsOverTime { get { return ElapsedTime.Ticks>0; } }
        // Jour disponibles ?
        public TimeSpan RemainingTime { get { return StartDateNC.Subtract(DateTime.Now); } }

      









        public static implicit operator RequestEntity(Request model)
        {
            RequestEntity r = null;
            using (var db = new NespeEntityContainer()) {
                r = (from t in db.Requests where t.Id == model.Id select t).FirstOrDefault();
                if (r == null) {
                    r = db.Requests.CreateObject();
                }
            }
            //public:b+{[^:b]+}:b+{[^:b]+}:b+.+
            //r.\2=model.\2;
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
            r.TransFrom = model.TransFrom;
            r.Kind = (short)model.kind;
            r.Parrain = model.Parrain;

            
            return r;
        }
        public static implicit operator Request(RequestEntity model)
        {
            Request r = new Request();
            r.Id = model.Id;
            r.SurnameNC = model.SurnameNC;
            r.NameNC = model.NameNC;
            r.DepartmentNC = model.DepartmentNC;
            r.FunctionNC = model.FunctionNC;
            r.SuperiorNC = model.SuperiorNC;
            r.BusinessStreamNC = model.BusinessStreamNC;
            r.StartDateNC = model.StartDateNC == null ? DateTime.Now : model.StartDateNC.Value;
            r.EmployeeNumberNC = model.EmployeeNumberNC;
            r.nonSAPNC = model.nonSAPNC == null ? true : model.nonSAPNC.Value;
            r.LocalNC = model.LocalNC;
            r.PhoneNC = model.PhoneNC;
            r.initialsNC = model.initialsNC;
            r.TransFrom = model.TransFrom;
            r.kind = (RequestKindEnum)(model.Kind == null ? 0 : model.Kind);
            r.Parrain = model.Parrain;


            return r;
        }
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