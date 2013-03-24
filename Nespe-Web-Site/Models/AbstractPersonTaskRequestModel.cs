using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Models
{
    public class AbstractPersonTaskRequestListModel<T> : AbstractListModel<T>
        where T : PersonTaskRequest
    {
    }
    public class AbstractPersonTaskRequestItemModel<T> : AbstractItemModel<T>
        where T : PersonTaskRequest
    {
        //public virtual IQueryable<Department> DepartmentList { get; set; }
    }
}