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
    [Table]
    public class Department
    {
        [Key, Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id { get; set; }
        [Column(IsVersion = true)]
        public int Version { get; set; }
        [Column]
        public string SID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public string EMail { get; set; }
        [Column]
        public string Phone { get; set; }
    }
	
}