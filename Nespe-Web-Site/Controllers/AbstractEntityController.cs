using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nespe.Application.WebSite.Models;
using Nespe.Data.Context;

namespace Nespe.Application.WebSite.Controllers
{
    public abstract class AbstractEntityController<T, LM, IM> : AbstractController
        where IM:AbstractItemModel<T>
        where LM : AbstractListModel<T>
    {
        private NespeDataContext currentDataContext=null;
        public virtual NespeDataContext CurrentDataContext(){
            if(currentDataContext!=null)
                return currentDataContext;
            return currentDataContext=CreateDataContext();
        }

        public virtual NespeDataContext CreateDataContext()
        {
 	        return new NespeDataContext{};
        }
        protected AbstractEntityController()
        {
        }
        public abstract T FindById(int id);
        public abstract IQueryable<T> FindBy();
        //protected abstract M CreateModel<M>(ControllerActionEnum action = ControllerActionEnum.Unknown) where M : AbstractModel;
        protected abstract LM CreateListModel(ControllerActionEnum action=ControllerActionEnum.Unknown);
        protected abstract IM CreateItemModel(ControllerActionEnum action=ControllerActionEnum.Unknown);
    }
    public enum ControllerActionEnum
    {
        Unknown, List, View, Details, Create, Read, Update, Delete
    }
}
