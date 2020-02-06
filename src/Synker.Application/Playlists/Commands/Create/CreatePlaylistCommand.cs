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
    public class CreatePlaylistCommand : IRequest<long>
    {
        public string Name { get; set; }

        public bool State { get; set; }

        public long? UserId { get; set; }

        public List<Media> Medias { get; }

        public class Handler : IRequestHandler<CreatePlaylistCommand, long>
        {
            private readonly ISynkerDbContext _context;
            private readonly IMediator _mediator;

            public Handler(ISynkerDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<long> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken: cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.UserId);
                }

                var entity = new Playlist
                {
                    Name = request.Name,
                    State = request.State ? OnlineState.Enabled : OnlineState.Disabled,
                    User = user
                };

                entity.AddRangeMedia(request.Medias);

                _context.Playlists.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new PlaylistCreated { PlaylistId = entity.Id }, cancellationToken);

                return entity.Id;
            }
        }
    }
}
