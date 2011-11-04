using System.Web.Mvc;
using UserData.Models;

namespace UserData.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [Authorize()]
        public ActionResult Dashboard(UserDataModel user) {
            ViewBag.Message = user.NomeCompleto;
            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
