﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nespe.Data.Entities
{
    public class Person
    {
        public virtual long Id { get; set; }
        public virtual int? Version { get; set; }
        public virtual string SID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FullName { get { return string.Concat(FirstName, ", ", LastName); } }
        public virtual string EMail { get; set; }
        public virtual string Phone { get; set; }
        public override string ToString()
        {
            return string.Concat(GetType().FullName,
                "{", "Id=", Id,
                ", Version=", Version,
                ", SID=\"", SID, "\"",
                ", Name=\"", FirstName, "\"",
                ", Description=\"", LastName, "\"",
                ", EMail=\"", EMail, "\"",
                ", Phone=\"", Phone, "\"",
                "}");
        }
    }
}
