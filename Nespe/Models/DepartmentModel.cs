using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nespe.Models
{
    public class DepartmentModel : IEnumerable<Department>
    {
        public List<Department> Items { get; set; }

        public Department Selected { get; set; }

        IEnumerator<Department> IEnumerable<Department>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
