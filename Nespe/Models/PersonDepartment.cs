using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace Nespe.Models
{
    [Table(Name="tbl_person_department")]
    public class PersonDepartment
    {
        [Key, Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id { get; set; }
        [Column(IsVersion = true)]
        public int Version { get; set; }

        [Column(Name = "Rank")]
        public long Rank { get; set; }
        [Column(Name = "Department_Id")]
        public long? Department_Id { get; set; }
        [Column(Name = "Person_Id")]
        public long? Person_Id { get; set; }
        [Column(Name = "Role_Id")]
        public PersonDepartmentRoleEnum Role_Id { get; set; }




        [Required(ErrorMessage = "Veuillez sélectionner un département")]
        [Display(Name = "Département")]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Department_Id")]
        public Department Department { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner une personne")]
        [Display(Name = "Personne")]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Person_Id")]
        public Person Person { get; set; }

    }

    public enum PersonDepartmentRoleEnum
    {
        Undefined, Assistant, Backup, Head, Manager
    }
}