﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Models
{
    public class ArrivalPersonTaskRequestListModel : AbstractPersonTaskRequestListModel<ArrivalPersonTaskRequest>
    {
    }
    public class ArrivalPersonTaskRequestItemModel : AbstractPersonTaskRequestItemModel<ArrivalPersonTaskRequest>
    {
        public virtual IQueryable<Department> DepartmentList { get; set; }
    }
}