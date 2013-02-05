using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nespe.Models
{
    public class RequestInfoModel : IEnumerable<RequestInfo>
    {
        public List<RequestInfo> Items { get; set; }

        public RequestInfo Selected { get; set; }

        IEnumerator<RequestInfo> IEnumerable<RequestInfo>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
