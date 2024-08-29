using System;
using System.Collections.Generic;

namespace bubbleTeaProject
{
    public partial class ProductsInOrder
    {
        public int? Amount { get; set; }
        public int ProdId { get; set; }
        public int OrderId { get; set; }

        public virtual Ordering Order { get; set; } = null!;
        public virtual Product Prod { get; set; } = null!;
    }
}
