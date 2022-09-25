using Microsoft.AspNetCore.Mvc;
using Playground.Application.Methods.Queries;
using Playground.WebAdmin.Models.Setting;

namespace Playground.WebAdmin.Controllers
{
    public class SettingController : BaseMvcController
    {
        public async Task<IActionResult> Index()
        {
            var query = new GetTagsQuery
            {
                IsPagingEnabled = false,
                Includes = new List<string> { "Color"}
            };
            var tagsResultModel = await Mediator.Send(query);

            return View(new SettingViewModel
            {
                Tags = tagsResultModel.Data.Items
            });
        }
    }
}
