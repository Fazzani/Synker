using MediatR;
using Synker.Application.Exceptions;
using Synker.Application.Interfaces;
using Synker.Domain.Entities;
using Synker.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Application.Playlists.Commands
{
    public class UpdatePlaylistCommand : IRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool State { get; set; }

        public List<Media> Medias { get; } 

        public class Handler : IRequestHandler<UpdatePlaylistCommand, Unit>
        {
            private readonly ISynkerDbContext _context;

            public Handler(ISynkerDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Playlists.FindAsync(new object[] { request.Id }, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Playlist), request.Id);
                }

                entity.Name = request.Name;
                entity.State = request.State ? OnlineState.Enabled : OnlineState.Disabled;

                //TODO: Update Medias

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
