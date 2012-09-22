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
    }

    public enum PersonDepartmentRoleEnum
    {
        Assistant, Backup, Head, Manager
    }
}