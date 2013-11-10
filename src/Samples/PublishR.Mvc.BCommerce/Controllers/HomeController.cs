using System.Web.Mvc;
using PublishR.Mvc.BCommerce.Models;
using PublishR.Mvc.BCommerce.ProductServiceReference;

namespace PublishR.Mvc.BCommerce.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            HomePageModel model = new HomePageModel();
            using (ProductServiceClient client = new ProductServiceClient()) {
                model.Products = client.GetProducts(new GetProductsRequest()).Products;
            }
            return View(model);
        }


        [HttpPost]
        public void CreateProduct(int val) {
            ProductServiceClient client = new ProductServiceClient();
        }
    }
}