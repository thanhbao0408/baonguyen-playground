using Microsoft.AspNetCore.Mvc;

namespace Playground.WebAdmin.Controllers
{
    public class SettingController : BaseMvcController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
