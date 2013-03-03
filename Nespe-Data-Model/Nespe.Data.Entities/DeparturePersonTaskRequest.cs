using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nespe.Data.Entities
{
    public class DeparturePersonTaskRequest : PersonTaskRequest
    {
        [Display(Name = "Initiales")]
        public string Initials { get; set; }

        [Required(ErrorMessage = "Veuillez entrer la date de départ du collaborateur")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }
        
        [Required(ErrorMessage = "Veuillez entrer le local")]
        public string Location { get; set; }

        public bool IsRetirement { get; set; }
    }
}
