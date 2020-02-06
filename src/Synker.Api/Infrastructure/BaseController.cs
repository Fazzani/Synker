namespace Synker.Api.Infrastructure
{
    using System.Net.Mime;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [ApiController]
    [Microsoft.AspNetCore.Mvc.ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class BaseController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
