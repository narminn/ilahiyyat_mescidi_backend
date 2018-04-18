using System.Web.Mvc;

namespace Ilahiyyat.Areas.Admin_Panel
{
    public class Admin_PanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin_Panel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_Panel_default",
                "Admin_Panel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}