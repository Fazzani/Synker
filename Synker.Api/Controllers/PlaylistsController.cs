using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synker.Api.Infrastructure;
using Synker.Application.Infrastructure.PagedResult;
using Synker.Application.Playlists.Commands;
using Synker.Application.Playlists.Queries;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Api.Controllers
{
    /// <summary>
    /// TODO: 
    /// - Caching playlists
    /// - Gestion auth and user
    /// </summary>
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

        /// <summary>
        /// Get playlist file
        /// </summary>
        /// <param name="id">playlist id</param>
        /// <param name="format">output file format</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}/format", Name = nameof(GetFormat))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFormat([FromRoute][Required]long id, [FromQuery] string format = "m3u",
            CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new PlaylistFileQuery { Id = id, FileFormat = format }, cancellationToken);

            if(string.IsNullOrEmpty(result))
            {
                return NoContent();
            }

            using (var playlistStream = new MemoryStream())
            using (var sw = new StreamWriter(playlistStream, Encoding.UTF8, 4096, true))
            {
                sw.Write(result);
                return File(playlistStream.ToArray(), "text/plain");
            }
        }
    }
}
