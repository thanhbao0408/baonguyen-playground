using Blog.Contracts.Dtos.Articles;
using Blog.Core.Methods.Queries;
using BN.CleanArchitecture.Infrastructure.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    public class BlogController : BaseController
    {
        #region Articles
        /*
         * Only expose the GET methods to the external APIs
         * GET Articles brief information 
         * GET Article
         * // POST Article
         * // PUT Article
         * // DELETE Article
         */

        [HttpGet("/api/v{version:apiVersion}/articles")]
        public async Task<ActionResult> HandleGetArticlesAsync([FromQuery] GetArticlesQuery query,
            CancellationToken cancellationToken = new())
        {
            //var queryModel = HttpContext.SafeGetListQuery<GetArticlesQuery, ListResultModel<ArticleDto>>(query);

            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("/api/v{version:apiVersion}/articles/{slug}")]
        public async Task<ActionResult<ArticleDetailDto>> HandleGetArticleBySlug([FromRoute] string slug, CancellationToken cancellation = new())
        {
            return Ok(await Mediator.Send(new GetArticleDetailBySlugQuery(slug), cancellation));
        }
        #endregion

        #region Tags
        /*
         * GET Tags
         * POST Tag
         * PUT Tag
         * GET Articles by Tag Id
         * DELETE Tag
         */
        #endregion

        #region TagColors
        /*
         * GET TagColors
         * POST TagColor
         * PUT TagColor
         * DELETE TagColor
         */
        #endregion

        #region Comments

        /*
         * GET Comments
         * GET Comments by Article Id
         * GET Comments by User Id
         * GET 
         */
        #endregion
    }
}
