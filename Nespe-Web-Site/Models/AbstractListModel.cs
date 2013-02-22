using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nespe.Application.WebSite.Models
{
    public abstract class AbstractListModel<T>:AbstractItemModel<T>, IQueryable<T>
    {
        public virtual IQueryable<T> ItemList { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            if (ItemList == null)
                return null;
            return ItemList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (ItemList == null)
                return null;
            return ItemList.GetEnumerator();
        }

        public Type ElementType
        {
            get { return ItemList.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return ItemList.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return ItemList.Provider; }
        }
    }
}