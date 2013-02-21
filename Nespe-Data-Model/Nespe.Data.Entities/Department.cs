using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nespe.Data.Entities
{
    public class Department
    {
        public virtual long Id { get; set; }
        public virtual int? Version { get; set; }
        public virtual string SID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Entity { get; set; }
        public virtual string EMail { get; set; }
        public virtual string Phone { get; set; }
        public virtual List<PersonDepartment> PersonList { get; set; }
        public override string ToString()
        {
            return string.Concat(GetType().FullName, 
                "{", "Id=", Id, 
                ", Version=", Version, 
                ", SID=\"", SID, "\"",
                ", Name=\"", Name, "\"",
                ", Description=\"", Description, "\"",
                ", Entity=\"", Entity, "\"",
                ", EMail=\"", EMail, "\"",
                ", Phone=\"", Phone, "\"", 
                "}");
        }
    }
}
