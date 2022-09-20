using Microsoft.AspNetCore.Mvc;

namespace Playground.WebAdmin.Controllers
{
    [Route("[controller]/[action]")]
    public class BlogController : BaseMvcController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
