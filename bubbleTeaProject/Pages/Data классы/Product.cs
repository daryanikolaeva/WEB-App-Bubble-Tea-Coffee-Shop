using System;
using System.Collections.Generic;

namespace bubbleTeaProject
{
    public partial class Product
    {
        public Product()
        {
            ProductsInOrders = new HashSet<ProductsInOrder>();
        }

        public int ProdId { get; set; }
        public string? ProdName { get; set; }
        public decimal? ProdPrice { get; set; }

        public virtual ICollection<ProductsInOrder> ProductsInOrders { get; set; }
    }
}
