using ClassActivity.Models;
using System.Web.Mvc;

namespace ClassActivity.Controllers
    {
    public class HomeController : Controller
        {


        public ActionResult Index()
            {
               return View();
            }

        public ActionResult Rubric()
            {
            return View();
            }
        }
    }