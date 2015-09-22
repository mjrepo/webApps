using System;
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

        public ActionResult Dashboard(int? year)
        {
            if (year.HasValue)
            {
                var startDate = new DateTime(year.Value, 1, 1);
                var endDate = startDate.AddYears(1).AddDays(-1);
                return PartialView(_calculatorService.CalculateSummary(startDate, endDate));
            }

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