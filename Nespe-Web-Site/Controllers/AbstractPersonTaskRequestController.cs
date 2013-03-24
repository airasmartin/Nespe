using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Application.WebSite.Models;
using Nespe.Data.Context;
using Nespe.Data.Entities;

namespace Nespe.Application.WebSite.Controllers
{
    public abstract class AbstractPersonTaskRequestController<T, L, I> : AbstractEntityController<T, L, I>
        where T : PersonTaskRequest
        where L : AbstractPersonTaskRequestListModel<T>
        where I : AbstractPersonTaskRequestItemModel<T>
    {
    }
}
