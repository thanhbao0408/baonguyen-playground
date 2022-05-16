
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    public class BlogController : Controller
    {
        #region Articles
        /*
         * GET Articles brief information 
         * GET Article
         * POST Article
         * PUT Article
         * DELETE Article
         */

        //public async Task<ActionResult> 
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
