using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Models
{
    public class DepartmentListModel : AbstractListModel<Department>
    {
    }
    public class DepartmentItemModel : AbstractItemModel<Department>
    {
        public virtual PersonListModel AvailablePersonList { get; set; }
    }
}