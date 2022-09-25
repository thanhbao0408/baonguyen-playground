using Microsoft.AspNetCore.Mvc;
using Playground.Application.Contracts.Dtos.Blog.Taggings;
using Playground.Application.Methods.Commands.Tags.CreateTag;
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
                Includes = new List<string> { "Color" }
            };
            var tagsResultModel = await Mediator.Send(query);

            var colorQuery = new GetTagColorsQuery();
            var tagColorsResultModel = await Mediator.Send(colorQuery);

            return View(new SettingViewModel
            {
                Tags = tagsResultModel.Data.Items,
                Colors = tagColorsResultModel.Data
            });
        }

        public async Task<IActionResult> CreateTag(CreateUpdateTagViewModel tag)
        {
            try
            {
                await Mediator.Send(new CreateTagCommand
                {
                    Model = new TagDto
                    {
                        Name = tag.Name,
                        ColorId = tag.ColorId,
                    }
                });
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> UpdateTag(CreateUpdateTagViewModel tag)
        {
            try
            {
                await Mediator.Send(new UpdateTagCommand
                {
                    Model = new TagDto
                    {
                        Id = tag.Id.Value,
                        Name = tag.Name,
                        ColorId = tag.ColorId,
                    }
                });
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
