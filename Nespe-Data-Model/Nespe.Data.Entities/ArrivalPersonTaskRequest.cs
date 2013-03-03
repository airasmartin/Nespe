using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nespe.Data.Entities
{
    public class ArrivalPersonTaskRequest : PersonTaskRequest
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
        [Required(ErrorMessage = "Veuillez entrer la date d'arrivée du nouveau collaborateur")]
        [Display(Name = "*Date d'arrivée")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime StartDate { get; set; }

        
    }
}
