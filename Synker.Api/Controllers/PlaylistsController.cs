using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Synker.Api.Infrastructure;
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
    }
}
