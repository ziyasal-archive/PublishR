using System.Web.Mvc;
using PublishR.Mvc.ACommerce.Models;
using PublishR.Mvc.ACommerce.ProductServiceReference;

namespace PublishR.Mvc.ACommerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomePageModel model = new HomePageModel();
            return View(model);
        }


        [HttpPost]
        public void Fire(int val)
        {
            ProductServiceClient client = new ProductServiceClient();
        }
    }
}