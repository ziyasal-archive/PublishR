using System.Web.Mvc;
using PublishR.Mvc.ACommerce.Models;
using PublishR.Mvc.ACommerce.ProductServiceReference;

namespace PublishR.Mvc.ACommerce.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            HomePageModel model = new HomePageModel();
            using (ProductServiceClient client = new ProductServiceClient()) {
                model.Products = client.GetProducts(new GetProductsRequest()).Products;
            }
            return View(model);
        }
    }
}