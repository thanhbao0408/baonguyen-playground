using Microsoft.AspNetCore.Mvc;
using Playground.Application.Contracts.Dtos.Blog.Articles;
using Playground.Application.Methods.Commands.Articles.CreateArticle;
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
            ArticleDetailDto articleDetail = null;
            if (!string.IsNullOrWhiteSpace(slug))
            {
                articleDetail = (await Mediator.Send(new GetArticleDetailBySlugQuery(slug))).Data;
            }

            if(articleDetail == null)
            {
                articleDetail = new ArticleDetailDto(Guid.Empty);
            };

            var articleDetailVM = new ArticleDetailVM
            {
                ArticleDetail = articleDetail
            };

            var query = new GetTagsQuery
            {
                IsPagingEnabled = false,
            };
            var tagsResultModel = await Mediator.Send(query);
            articleDetailVM.Tags = tagsResultModel.Data.Items;

            return Details(articleDetailVM);
        }

        private IActionResult Details(ArticleDetailVM articleVM)
        {
            return View("Details", articleVM);
        }

        public async Task<IActionResult> Modify(ArticleDetailVM articleDetailVM)
        {
            try
            {
                ArticleDetailDto articleDto;
                // Sucess, stay on this page;
                if (articleDetailVM.ArticleDetail.Id == Guid.Empty)
                {
                    // create new
                    var createArticleCommand = new CreateArticleCommand
                    {
                        Model = articleDetailVM.ArticleDetail
                    };
                    articleDto = (await Mediator.Send(createArticleCommand)).Data;
                }
                else
                {
                    // update
                    var updateArticleCommand = new UpdateArticleCommand
                    {
                        Model = articleDetailVM.ArticleDetail
                    };
                    articleDto = (await Mediator.Send(updateArticleCommand)).Data;
                }
                //return await this.Details(articleDetailVM.ArticleDetail.Slug);
                return RedirectToAction("Details", new { slug = articleDetailVM.ArticleDetail.Slug });
            }
            catch(Exception ex)
            {
                return Details(articleDetailVM);
            }
        }
    }
}
