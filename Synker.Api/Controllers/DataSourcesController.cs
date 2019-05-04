using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synker.Api.Infrastructure;
using Synker.Application.DataSources.Queries.GetDatasource;
using Synker.Application.DataSources.Queries.GetListDatasource;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Api.Controllers
{
    public class DataSourcesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ListDatasourceViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromBody] ListDatasourceQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DataSourceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetDataSourceQuery { Id = id }, cancellationToken));
        }
    }
}
