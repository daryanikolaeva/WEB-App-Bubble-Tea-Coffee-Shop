using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bubbleTeaProject.Pages
{
    public class ProductPageModel : PageModel
    {
        public int Parameter;
        public List<Product> products = new List<Product>();
        public Product product = new Product();
        public List<String> images = new List<String>()
        {
            "~/images/products/strawberry.png",
            "~/images/products/cofee.png",
            "~/images/products/green.png",
            "~/images/products/flowers.png",
            "~/images/products/unicorn.png",
            "~/images/products/bumbl.png"
        };

        public List<String> description = new List<String>()
        {
            "��������� ��������� �������� � ������ � ����������� ��������",
            "�������� ������� � �������� � ������� ������ ������",
            "�������� ���� � ��������� ������� ��������",
            "����� � ��������� ���������� ��� � ������� �������",
            "��������� ������� � ��������� � ��������� ������������",
            "����� ������ �� ����� � ������������� ��������"
        };

        public int Amount;
        public void OnGet(int parameter, int amount)
        {
            Parameter = parameter;
            Amount = amount;
                LoadProducts();
            
        }

        public IActionResult OnPost(int parameter, int amount, string action)
        {
            if (action == "increment")
            {
                
                var newAmount = amount+1;

                return RedirectToPage("/ProductPage", new { parameter = parameter, amount = newAmount});

            }
            else
            {
                if (action == "decrement" && amount>0)
                {
                    var newAmount = amount - 1;

                    return RedirectToPage("/ProductPage", new { parameter = parameter, amount = newAmount });

                }
                else 
                if (amount>0)
                {
                    
                    using (bubble_teaContext db = new bubble_teaContext())
                    {
                        var currentProduct = db.Products.FirstOrDefault(p => p.ProdId == parameter);

                        if (!Program.IsSelected)
                       {

                            //�������� ����� �����
                            var newOrder = new Ordering()
                            {
                                OrderPrice = currentProduct.ProdPrice * amount
                            };
                            db.Orderings.Add(newOrder);
                            db.SaveChanges();

                            //����� ������ � ���������������
                            var productInOrder = new ProductsInOrder()
                            {
                                Amount = amount,
                                Order = newOrder,
                                Prod =  currentProduct
                            };
                            db.ProductsInOrders.Add(productInOrder);
                            db.SaveChanges();
                            Program.IsSelected = true;
                       }
                       else {
                            //����� ���������� ���������� ���������� � ������� ��������
                            var currentOrder = db.Orderings.OrderByDescending(o => o.OrderId).FirstOrDefault();
                            //���������� ������ � ���������������
                            var productInOrder = new ProductsInOrder()
                            {
                                Amount = amount,
                                Order = currentOrder,
                                Prod = currentProduct
                            };
                            db.ProductsInOrders.Add(productInOrder);

                            //���������� OrderPrice
                            currentOrder.OrderPrice += currentProduct.ProdPrice * amount;
                            db.Orderings.Update(currentOrder); 

                            db.SaveChanges();
                       }

                        db.SaveChanges();
                    }
                }
            }
            return RedirectToPage("/ProductPage", new { parameter = parameter });
        }


        public void LoadProducts()
        {
            using (bubble_teaContext db = new bubble_teaContext())
            {
                products= db.Products.AsNoTracking().ToList();
                //product = products[Parameter];
                product = products.FirstOrDefault(p => p.ProdId == Parameter);
            }

        }
        
        
    }
}
