using Microsoft.AspNetCore.Mvc;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Application.Methods.Queries;
using Playground.WebAdmin.Models.Blog;

namespace Playground.WebAdmin.Controllers
{
    public class BlogController : BaseMvcController
    {
        public async Task<IActionResult> Index()
        {
            var query = new GetArticlesQuery
            {
                IsPagingEnabled = false,
            };
            var articles = await Mediator.Send(query);

            var manageBlogVM = new ManageBlogViewModel
            {
                Articles = articles.Data.Items
            };
            return View(manageBlogVM);
        }

        public async Task<IActionResult> Details(string slug)
        {
            ArticleDetailDto articleDetails = null;
            if (!string.IsNullOrWhiteSpace(slug))
            {
                articleDetails = (await Mediator.Send(new GetArticleDetailBySlugQuery(slug))).Data;
            }
            return View(articleDetails);
        }
    }
}
