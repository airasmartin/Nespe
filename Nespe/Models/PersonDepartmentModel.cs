using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nespe.Models
{
    public class PersonDepartmentModel : IEnumerable<PersonDepartment>
    {
        public List<PersonDepartment> Items { get; set; }

        public PersonDepartment Selected { get; set; }

        IEnumerator<PersonDepartment> IEnumerable<PersonDepartment>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
