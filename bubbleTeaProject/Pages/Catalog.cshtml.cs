using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bubbleTeaProject.Pages
{
    public class CatalogModel : PageModel
    {
        public List<String> names = new List<String>();
        public void OnGet()
        {
        }

        public void LoadingData()
        {
            using (bubble_teaContext db = new bubble_teaContext() )
            {
                List<Product> products = new List<Product>();
                products = db.Products.ToList();
                for (int i = 0; i< products.Count;i++)
                {
                    names.Add(products[i].ProdName);
                }
            }
        }
    }
}
