using System.Web.Mvc;
using AngularSample.Areas.CarCalculator.Services;

namespace AngularSample.Areas.CarCalculator.Controllers
{
    public class CarCalculatorController : Controller
    {
        private readonly CarCalculatorService _calculatorService = new CarCalculatorService();
        // GET: CarCalculator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewEntry()
        {
            return PartialView();
        }

        public ActionResult Dashboard()
        {
            return PartialView(_calculatorService.CalculateSummary());
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