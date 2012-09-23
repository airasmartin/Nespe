using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nespe.Models
{
    public class PersonModel : IEnumerable<Person>
    {
        public PersonDepartmentModel DepartmentModel { get; set; }
        
        public List<Person> Items { get; set; }

        public Person Selected { get; set; }

        IEnumerator<Person> IEnumerable<Person>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
