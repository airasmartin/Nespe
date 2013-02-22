using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Models
{
    public class PersonListModel : AbstractListModel<Person>
    {
    }
    public class PersonItemModel : AbstractItemModel<Person>
    {
        public virtual DepartmentListModel AvailableDepartmentList { get; set; }

        public virtual PersonDepartmentListModel DepartmentList { get; set; }
    }
}