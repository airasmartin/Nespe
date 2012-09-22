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
    [Table(Name = "tbl_person")]
    public class Person
    {
        [Key, Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id { get; set; }
        [Column(IsVersion = true)]
        public int Version { get; set; }

        [Column]
        public string SID { get; set; }
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }
        [Column]
        public string EMail { get; set; }
        [Column]
        public string Phone { get; set; }
    }
	
}