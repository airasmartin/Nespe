﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nespe.Models
{

    public class CurrentStatusModel
    {
        public IQueryable<Request> RequestSet { get; set; }
        public IQueryable<Request> FinishedRequestSet { get { return (from t in RequestSet where t.Id > 0 && t.IsFinished == true select t); } }
        public IQueryable<Request> CompletionRequestSet { get { return (from t in RequestSet where t.Id > 0 && t.Completed == false && t.IsFinished == false select t); } }
        public IQueryable<Request> AdministrationRequestSet { get { return (from t in RequestSet where t.Id > 0 select t); } }
        public IQueryable<Request> ArrivalRequestSet { get { return (from t in RequestSet where t.Completed == true && t.Kind == RequestKindEnum.Arrival && t.IsFinished == false select t); } }
        public IQueryable<Request> DepartureRequestSet { get { return (from t in RequestSet where t.Completed == true && t.Kind == RequestKindEnum.Departure && t.IsFinished == false select t); } }
        public IQueryable<Request> TransfertRequestSet { get { return (from t in RequestSet where t.Completed == true && t.Kind == RequestKindEnum.Transfert && t.IsFinished == false select t); } }

    }
}