using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bubbleTeaProject.Pages
{
    public class CartModel : PageModel
    {
        public List<String> images = new List<String>()
        {
            "~/images/order/order_0.png",
            "~/images/order/order_1.png",
            "~/images/order/order_2.png",
            "~/images/order/order_3.png",
            "~/images/order/order_4_new.png",
            "~/images/order/order_5.png",
        };

        public List<ProductsInOrder> Check = new List<ProductsInOrder>();
        public Ordering LastOrder = new Ordering();
        public List<Product> Products = new List<Product>();
        public List<Decimal?> TotalCountEachProduct = new List<Decimal?>();

        public void OnGet()
        {
           GetOrder();
           CountEachProduct();
        }

        public void GetOrder()
        {
            if(Program.IsSelected)
            using (bubble_teaContext db = new bubble_teaContext())
            {
                //получение номера последнего заказа
                LastOrder = db.Orderings.OrderByDescending(o => o.OrderId).FirstOrDefault();

                //получение списка продуктов в заказе
                Check = db.ProductsInOrders.Where(p => p.OrderId == LastOrder.OrderId).ToList();


                foreach (var prod in Check)
                {
                    var product = db.Products.FirstOrDefault(p => p.ProdId == prod.ProdId);
                    Products.Add(product);
                }

            }


        }

        public void CountEachProduct()
        {
            for(int i=0;i< Products.Count; i++)
            {
                TotalCountEachProduct.Add(Products[i].ProdPrice * Check[i].Amount);
            }
        }

        public void OnPost(string username, int tel)
        {
            using (bubble_teaContext db = new bubble_teaContext())
            {
                if (!FindCustomer(tel))
                {
                    db.Customers.Add(new Customer()
                    {
                        TelNum = tel,
                        Name = username,
                    });
                    db.SaveChanges();
                }

                var last = db.Orderings.OrderByDescending(o => o.OrderId).FirstOrDefault();
                last.TelNum = tel;
                db.Orderings.Update(last);
                db.SaveChanges();


            }

            Program.IsSelected = false;
        }

        public bool FindCustomer(int tel)
        {
            using (bubble_teaContext db = new bubble_teaContext())
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.TelNum == tel);

                if (customer != null)
                    return true;
                return false;
            }
        }
    }
}
