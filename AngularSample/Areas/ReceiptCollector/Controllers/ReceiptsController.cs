using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularSample.Areas.ReceiptCollector.Controllers
{
    public class ReceiptsController : Controller
    {
        // GET: ReceiptCollector/Receipts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddArticles()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddArticles(object model)
        {
            return View();
        }
    }
}