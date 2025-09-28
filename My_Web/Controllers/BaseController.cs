using Microsoft.AspNetCore.Mvc;

namespace My_Web.Controllers
{
    public class BaseController : Controller
    {
        protected void SetLayout()
        {
            if (User.IsInRole("Admin"))
                ViewData["Layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";
            else
                ViewData["Layout"] = "~/Views/Shared/_LayoutUser.cshtml";
        }
    }
}
