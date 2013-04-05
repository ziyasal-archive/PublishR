using System.Web.Mvc;
using PublishR.Mvc.SampleNode2.Models;
using PublishR.Mvc.SampleNode2.ProductServiceReference;

namespace PublishR.Mvc.SampleNode2.Controllers
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
            client.SetData(val);
        }
    }
}