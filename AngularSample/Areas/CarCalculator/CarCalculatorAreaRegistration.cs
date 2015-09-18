using System.Web.Mvc;

namespace AngularSample.Areas.CarCalculator
{
    public class CarCalculatorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CarCalculator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CarCalculator_default",
                "CarCalculator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}