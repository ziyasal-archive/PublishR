using System.Web;
using System.Web.Mvc;

namespace PunlishR.Server.Api {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}