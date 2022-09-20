using Microsoft.AspNetCore.Mvc;

namespace Playground.WebAdmin.Controllers
{
    public class BlogController : BaseMvcController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
