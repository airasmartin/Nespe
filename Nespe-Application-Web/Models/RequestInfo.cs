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
    [Table("tbl_requestInfo")]
    public partial class RequestInfo
    {
        [Key()]
        public long Id { get; set; }
        public long Request_Id { get; set; }
        public long Type_Id { get; set; }
        public string Name { get; set; }
        public string Executor { get; set; }
        public StatusRequestInfoEnum Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public RequestInfo Copy(RequestInfo src, bool copyId = false)
        {
            var dst = this;
            if (copyId) dst.Id = src.Id;
            dst.Request_Id = src.Request_Id;
            dst.Type_Id = src.Type_Id;
            dst.Name = src.Name;
            dst.Executor = src.Executor;
            dst.Status = src.Status;
            dst.Date = src.Date;
            dst.Comment = src.Comment;
            return src;
        }
    }	
}