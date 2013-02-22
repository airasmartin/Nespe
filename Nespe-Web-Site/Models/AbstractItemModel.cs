using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nespe.Application.WebSite.Models
{
    public abstract class AbstractItemModel<T>:AbstractModel
    {
        public virtual T ItemSelected { get; set; }
        public static implicit operator T(AbstractItemModel<T> me)
        {
            return me.ItemSelected;
        }
    }
}