using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularSample.Controllers
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