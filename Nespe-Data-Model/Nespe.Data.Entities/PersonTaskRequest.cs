using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nespe.Data.Entities
{
    public class PersonTaskRequest:TaskRequest
    {
        [Required]
        public virtual Person Person { get; set; }
        [Required]
        public virtual Department Department { get; set; }

        public override string ToString()
        {
            return string.Concat(GetType().FullName,
                "{", "Id=", Id,
                ", Version=", Version,
                ", TypeId=\"", TypeId, "\"",
                ", Person={", Person, "}",
                ", Department=(", Department, "}",
                ", Name=\"", Name, "\"",
                ", Status=\"", Status, "\"",
                ", Date=\"", Date, "\"",
                ", Comment=\"", Comment, "\"",
                "}");
        }
    }
}
