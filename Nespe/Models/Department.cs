using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
//using System.Data.Linq.Mapping;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Data.Objects.DataClasses;

namespace Nespe.Models
{
    [Table("tbl_department")]
    [System.Data.Linq.Mapping.Table(Name = "tbl_department")]
    //[EdmEntityTypeAttribute(NamespaceName = "Nespe.Models", Name = "Department")]
    public class Department
    {
        [Key, Column]//, EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
        [System.Data.Linq.Mapping.Column(IsPrimaryKey=true, IsDbGenerated=true)]
        public long Id { get; set; }
        [Column][System.Data.Linq.Mapping.Column]
        public string SID { get; set; }
        [Column][System.Data.Linq.Mapping.Column]
        public string Name { get; set; }
        [Column][System.Data.Linq.Mapping.Column]
        public string Description { get; set; }
        [Column]
        [System.Data.Linq.Mapping.Column]
        public string EMail { get; set; }
        [Column]
        [System.Data.Linq.Mapping.Column]
        public string Phone { get; set; }

        public Department Copy(Department src, bool copyId=false) {
            var dst=this;
            if (copyId) dst.Id = src.Id;
            dst.SID = src.SID;
            dst.Name = src.Name;
            dst.Description = src.Description;
            dst.EMail = src.EMail;
            dst.Phone = src.Phone;
            return src;
        }
    }
	
}