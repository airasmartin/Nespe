using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
//using System.Data.Linq.Mapping;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.AssociationAttribute;
using System.Collections;
using System.Data.Objects.DataClasses;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;
//using AssociationAttribute = System.Data.Linq.Mapping.AssociationAttribute;

namespace Nespe.Models
{
    [Table("tbl_person_department")]
    //[EdmEntityTypeAttribute(NamespaceName = "Nespe.Models", Name = "PersonDepartment")]
    public class PersonDepartment
    {
        [Key]//, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
        public long Id { get; set; }

        public long Rank { get; set; }
        [Column("Department_Id")]
        [ForeignKey("Department")]
        public long Department_Id { get; set; }

        [Column("Person_Id")]
        [ForeignKey("Person")]
        //[System.Data.Linq.Mapping.Column]
        public long Person_Id { get; set; }

        //[Column("Role"), EnumDataType(typeof(PersonDepartmentRoleEnum))]
        [NotMapped]
        [System.Data.Linq.Mapping.Column]
        public virtual PersonDepartmentRoleEnum Role { get { return (PersonDepartmentRoleEnum)RoleValue; } set { RoleValue = (uint)value; } }
        [Column("Role")]
        public virtual uint RoleValue { get; set; }




        //[Required(ErrorMessage = "Veuillez sélectionner un département")]
        [Display(Name = "Département")]
        //[System.ComponentModel.DataAnnotations.Schema.Column("Department_Id")]
        [ForeignKey("Id")]
        //[System.Data.Linq.Mapping.Association(IsForeignKey = true, Storage = "Department_Id", ThisKey = "Department_Id", OtherKey = "Id")]
        public virtual Department Department { get; set; }

        //[Required(ErrorMessage = "Veuillez sélectionner une personne")]
        [Display(Name = "Personne")]
        //[System.ComponentModel.DataAnnotations.Schema.Column("Person_Id")]
        [ForeignKey("Id")]
        public virtual Person Person { get ; set; }


        public PersonDepartment Copy(PersonDepartment src, bool copyId = false)
        {
            var dst = this;
            if (copyId) dst.Id = src.Id;
            dst.Rank = src.Rank;
            dst.Role = src.Role;
            if (dst.Person == null)
                dst.Person = src.Person;
            else
                dst.Person.Copy(src.Person);

            if (dst.Department == null)
                dst.Department = src.Department;
            else
                dst.Department.Copy(src.Department);
            return src;
        }
    }

    public enum PersonDepartmentRoleEnum:uint
    {
        Undefined, Assistant, Backup, Head, Manager
    }
}