using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using AssociationAttribute = System.Data.Linq.Mapping.AssociationAttribute;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Data.Objects.DataClasses;

namespace Nespe.Models
{
    [Table("tbl_person")]
    [System.Data.Linq.Mapping.Table(Name = "tbl_person")]
    //[EdmEntityTypeAttribute(NamespaceName = "Nespe.Models", Name = "Person")]
    public partial class Person
    {
        [Key, Column]//, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
        [System.Data.Linq.Mapping.Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id { get; set; }

        [Column]
        [System.Data.Linq.Mapping.Column]
        public string SID { get; set; }
        [Column]
        [System.Data.Linq.Mapping.Column]
        [Required(ErrorMessage = "Please enter the first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Column]
        [System.Data.Linq.Mapping.Column]
        [Required(ErrorMessage = "please enter the name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Column]
        [System.Data.Linq.Mapping.Column]
        public string EMail { get; set; }
        [Column]
        [System.Data.Linq.Mapping.Column]
        public string Phone { get; set; }
        [Column]
        [System.Data.Linq.Mapping.Column]
        public string Initials { get; set; }

        public Person Copy(Person src, bool copyId = false)
        {
            var dst = this;
            if (copyId) dst.Id = src.Id;
            dst.SID = src.SID;
            dst.FirstName = src.FirstName;
            dst.LastName = src.LastName;
            dst.EMail = src.EMail;
            dst.Phone = src.Phone;
            return src;
        }

    }
	
}