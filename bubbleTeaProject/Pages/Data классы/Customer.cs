using System;
using System.Collections.Generic;

namespace bubbleTeaProject
{
    public partial class Customer
    {
        public Customer()
        {
            Orderings = new HashSet<Ordering>();
        }

        public int TelNum { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Ordering> Orderings { get; set; }
    }
}
