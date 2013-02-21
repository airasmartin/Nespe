using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nespe.Data.Entities
{
    public class PersonDepartment
    {
        public virtual long Id { get; set; }
        public virtual int? Version { get; set; }
        public virtual PersonDeparmentRoleEnum Role { get { return (PersonDeparmentRoleEnum)RoleId; } set { RoleId = (short) value; } }
        public virtual short RoleId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Department Department { get; set; }
        public override string ToString()
        {
            return string.Concat(GetType().FullName,
                "{", "Id=", Id,
                ", Version=", Version,
                ", Role=\"", Role, "\"",
                ", RoleId=", RoleId,
                ", Person=", Person,
                ", Department=", Department,
                "}");
        }
        public static implicit operator Person(PersonDepartment me)
        {
            return me==null?null:me.Person;
        }
        public static implicit operator Department(PersonDepartment me)
        {
            return me == null ? null : me.Department;
        }
        public static implicit operator PersonDeparmentRoleEnum(PersonDepartment me)
        {
            return me == null ? PersonDeparmentRoleEnum.Undefined : me.Role;
        }
    }
    public enum PersonDeparmentRoleEnum : ushort
    {
        Undefined, Assistant, Backup, Head, Manager
    }
}
