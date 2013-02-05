using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nespe.Models
{
    public class RequestTypeModel : IEnumerable<RequestType>
    {
        public List<RequestType> Items { get; set; }

        public RequestType Selected { get; set; }

        IEnumerator<RequestType> IEnumerable<RequestType>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
