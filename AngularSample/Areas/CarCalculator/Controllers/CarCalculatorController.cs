using System.Web.Mvc;

namespace AngularSample.Areas.CarCalculator.Controllers
{
    public class CarCalculatorController : Controller
    {
        // GET: CarCalculator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewEntry()
        {
            return PartialView();
        }

        public ActionResult Cars()
        {
            return PartialView();
        }

        public ActionResult List()
        {
            return PartialView();
        }

        public ActionResult Statistics()
        {
            return PartialView();
        }
    }
}