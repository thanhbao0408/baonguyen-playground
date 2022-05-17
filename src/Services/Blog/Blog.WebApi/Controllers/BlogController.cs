
using Blog.Contracts.Dtos.Articles;
using Blog.Core.Queries;
using BN.CleanArchitecture.Core.Domain.Cqrs;
using BN.CleanArchitecture.Infrastructure;
using BN.CleanArchitecture.Infrastructure.Controller;
using MediatR;
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
         * GET Articles brief information 
         * GET Article
         * POST Article
         * PUT Article
         * DELETE Article
         */

        [HttpGet("/api/v{version:apiVersion}/articles")]
        public async Task<ActionResult> HandleGetArticlesAsync([FromQuery] GetArticlesQuery query,
            CancellationToken cancellationToken = new())
        {
            //var queryModel = HttpContext.SafeGetListQuery<GetArticlesQuery, ListResultModel<ArticleDto>>(query);

            return Ok(await Mediator.Send(query,cancellationToken));
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
