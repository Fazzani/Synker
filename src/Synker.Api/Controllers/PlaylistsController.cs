using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synker.Api.Infrastructure;
using Synker.Application.Infrastructure.PagedResult;
using Synker.Application.Playlists.Commands;
using Synker.Application.Playlists.Queries;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Api.Controllers
{
    public class PlaylistsController : BaseController
    {
        /// <summary>
        /// Listing playlists without they medias
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("list")]
        [ProducesResponseType(typeof(PlaylistLookupViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromBody] ListPlaylistQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpGet("{id}", Name = nameof(GetPlaylist))]
        [ProducesResponseType(typeof(PlaylistViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlaylist(long id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetPlaylistQuery { Id = id }, cancellationToken));
        }

        /// <summary>
        /// Listing playlists with they medias
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{id}/medias", Name = "GetPlaylistWithMedias")]
        [ProducesResponseType(typeof(PagedResult<PlaylistMediasViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWithMedias([FromRoute][Required]long id, [FromBody] PlaylistMediasQuery query, CancellationToken cancellationToken = default)
        {
            query.Id = id;
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreatePlaylistCommand cmd, CancellationToken cancellationToken)
        {
            var PlaylistId = await Mediator.Send(cmd, cancellationToken);
            return CreatedAtRoute(nameof(GetPlaylist), new { id = PlaylistId }, cmd);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] UpdatePlaylistCommand cmd, CancellationToken cancellationToken)
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
            await Mediator.Send(new DeletePlaylistCommand { Id = id });

            return NoContent();
        }
    }
}
