using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nespe.Data.Entities
{
    public class TransfertPersonTaskRequest : ArrivalPersonTaskRequest
    {
        [Display(Name = "Transféré de")]
        public string TransFrom { get; set; }

        public override string ToString()
        {
            return string.Concat(GetType().FullName,
                "{", "Id=", Id,
                ", Version=", Version,
                ", TypeId=\"", TypeId, "\"",
                ", Name=\"", Name, "\"",
                ", Status=\"", Status, "\"",
                ", Date=\"", Date, "\"",
                ", Comment=\"", Comment, "\"",
                "}");
        }
    }
}
