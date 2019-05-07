using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synker.Api.Infrastructure;
using Synker.Application.Playlists.Commands.Create;
using Synker.Application.Playlists.Commands.Delete;
using Synker.Application.Playlists.Commands.Update;
using Synker.Application.Playlists.Queries;

namespace Synker.Api.Controllers
{
    public class PlaylistsController : BaseController
    {
        //[HttpGet]
        //[ProducesResponseType(typeof(ListPlaylistViewModel), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetList([FromBody] ListPlaylistQuery query, CancellationToken cancellationToken)
        //{
        //    return Ok(await Mediator.Send(query, cancellationToken));
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlaylistViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetPlaylistQuery { Id = id }, cancellationToken));
        }


        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreatePlaylistCommand cmd, CancellationToken cancellationToken)
        {
            var PlaylistId = await Mediator.Send(cmd, cancellationToken);
            return CreatedAtRoute(nameof(Get), new { id = PlaylistId }, cmd);
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
