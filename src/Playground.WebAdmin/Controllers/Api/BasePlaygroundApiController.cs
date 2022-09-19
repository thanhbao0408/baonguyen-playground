using BN.CleanArchitecture.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Playground.WebAdmin.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BasePlaygroundApiController : BaseApiController
    {
    }
}
