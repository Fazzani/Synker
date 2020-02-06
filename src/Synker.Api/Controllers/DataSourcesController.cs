using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synker.Api.Infrastructure;
using Synker.Application.DataSources.Commands.Create;
using Synker.Application.DataSources.Commands.Delete;
using Synker.Application.DataSources.Commands.Update;
using Synker.Application.DataSources.Queries;
using Synker.Application.DataSources.Queries.GetDatasource;
using Synker.Application.DataSources.Queries.GetListDatasource;
using Synker.Application.Infrastructure.PagedResult;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Api.Controllers
{
    public class DataSourcesController : BaseController
    {
        /// <summary>
        /// Getting datasources without they medias
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("list")]
        [ProducesResponseType(typeof(PagedResult<DatasourceLookupViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromBody] ListDatasourceQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("{id:long}", Name = nameof(GetDataSource))]
        [ProducesResponseType(typeof(DataSourceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDataSource([FromRoute][Required]long id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetDataSourceQuery { Id = id }, cancellationToken));
        }

        /// <summary>
        /// Getting datasources with they medias
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{id}/medias", Name = nameof(GetWithMedias))]
        [ProducesResponseType(typeof(PagedResult<DataSourceMediasViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWithMedias([FromRoute][Required]long id, [FromBody] DataSourceMediasQuery query, CancellationToken cancellationToken = default)
        {
            query.DataSourceId = id;
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateDataSourceCommand cmd, CancellationToken cancellationToken)
        {
            var dataSourceId = await Mediator.Send(cmd, cancellationToken);

            return CreatedAtRoute(nameof(GetDataSource), new { id = dataSourceId }, cmd);
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
