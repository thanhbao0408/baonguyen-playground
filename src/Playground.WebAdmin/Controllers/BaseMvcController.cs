using BN.CleanArchitecture.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Playground.WebAdmin.Controllers
{
    [Authorize] // Leave the constructor empty to use the default scheme. i.g. IdentityConstants.ApplicationScheme
    public class BaseMvcController : BaseController
    {
    }
}
