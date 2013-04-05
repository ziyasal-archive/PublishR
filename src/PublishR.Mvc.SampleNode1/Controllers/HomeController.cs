using System.Web.Mvc;
using PublishR.Mvc.SampleNode1.Models;
using PublishR.Mvc.SampleNode1.ProductServiceReference;

namespace PublishR.Mvc.SampleNode1.Controllers
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
            client.SetData(20);
        }
    }
}