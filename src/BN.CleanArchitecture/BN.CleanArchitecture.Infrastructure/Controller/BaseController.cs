using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BN.CleanArchitecture.Infrastructure.Controller;

[ApiController]
public class BaseController : Microsoft.AspNetCore.Mvc.Controller
{
    private ISender _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}