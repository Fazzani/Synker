using Microsoft.AspNetCore.Mvc;
using Synker.Api.Infrastructure;
using Synker.Application.DataSources.Queries;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Api.Controllers
{
    public class DataSourcesController : BaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DataSourceViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetDataSourceQuery { Id = id }, cancellationToken));
        }
    }
}
