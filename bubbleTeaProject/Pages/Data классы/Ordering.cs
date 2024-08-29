using System;
using System.Collections.Generic;

namespace bubbleTeaProject
{
    public partial class Ordering
    {
        public Ordering()
        {
            ProductsInOrders = new HashSet<ProductsInOrder>();
        }

        public int OrderId { get; set; }
        public int? TelNum { get; set; }
        public decimal? OrderPrice { get; set; }

        public virtual Customer? TelNumNavigation { get; set; }
        public virtual ICollection<ProductsInOrder> ProductsInOrders { get; set; }
    }
}
