﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synker.Api.Infrastructure;
using Synker.Application.DataSources.Commands.Create;
using Synker.Application.DataSources.Commands.Delete;
using Synker.Application.DataSources.Commands.Update;
using Synker.Application.DataSources.Queries.GetDatasource;
using Synker.Application.DataSources.Queries.GetListDatasource;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Api.Controllers
{
    public class DataSourcesController : BaseController
    {
        [HttpPost("list")]
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

        [HttpPost]
        [ProducesResponseType(typeof(ListDatasourceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateDataSourceCommand cmd, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(cmd, cancellationToken));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] UpdateDataSourceCommand cmd, CancellationToken cancellationToken)
        {
            cmd.Id = id;
            await Mediator.Send(cmd, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteDataSourceCommand { Id = id });

            return NoContent();
        }
    }
}
