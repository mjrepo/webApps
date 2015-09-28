using System.Web.Mvc;

namespace AngularSample.Areas.ReceiptCollector
{
    public class ReceiptCollectorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ReceiptCollector";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ReceiptCollector_default",
                "ReceiptCollector/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}