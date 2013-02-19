using System;
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
        public virtual string EMail { get; set; }
        public virtual string Phone { get; set; }
    }
}
