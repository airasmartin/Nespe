using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
//using System.Data.Linq.Mapping;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace Nespe.Models
{
    [Table("tbl_requestType")]
    public class RequestType
    {
        [Key(), Column]
        public long Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        public RequestType Copy(RequestType src, bool copyId = false)
        {
            var dst = this;
            if (copyId) dst.Id = src.Id;
            dst.Name = src.Name;
            dst.Description = src.Description;
            return src;
        }
    }
 	
}