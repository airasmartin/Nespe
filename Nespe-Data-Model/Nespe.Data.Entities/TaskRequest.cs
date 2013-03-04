using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Nespe.Data.Entities
{
    public class TaskRequest
    {
        public virtual long Id { get; set; }
        public virtual int? Version { get; set; }
        public virtual long Rank { get; set; }
        public virtual long TypeId { get; set; }
        public virtual string Name { get; set; }
        public virtual TaskRequestStatusEnum Status { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Comment { get; set; }
        public virtual string SapPositionID { get; set; }
        public virtual string CostCenter { get; set; }
        //public virtual string Parrain { get; set; }
        public override string ToString()
        {
            return string.Concat(GetType().FullName,
                "{", "Id=", Id,
                ", Version=", Version,
                ", TypeId=\"", TypeId, "\"",
                ", Name=\"", Name, "\"",
                ", Status=\"", Status, "\"",
                ", Date=\"", Date, "\"",
                ", Comment=\"", Comment, "\"",
                ", SapPositionID=\"", SapPositionID, "\"",
                ", CostCenter=\"", CostCenter, "\"",
                //", Parrain=\"", Parrain, "\"",
                "}");
        }
    }
    public enum TaskRequestKindEnum {
        [Display(Name = "nouveau arrivé")]
        Arrival=0,
        [Display(Name = "Transfert de ")]
        Transfert = 1,
        [Display(Name = "Bye bye")]
        Departure=2
    }
    public enum TaskRequestStatusEnum
    {
        [Display(Name = "nouveau arrivé")]
        Initier = 0,
        [Display(Name = "Transfert de ")]
        Completer = 1,
        [Display(Name = "Bye bye")]
        Arrival = 2
    }

}
