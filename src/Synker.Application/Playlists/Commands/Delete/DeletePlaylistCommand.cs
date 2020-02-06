namespace Synker.Application.Playlists.Commands
{
    using MediatR;
    using Synker.Application.Exceptions;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeletePlaylistCommand : IRequest
    {
        public long Id { get; set; }

        public class Handler : IRequestHandler<DeletePlaylistCommand, Unit>
        {
            private readonly ISynkerDbContext _context;

            public Handler(ISynkerDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Playlists.FindAsync(new object[] { request.Id }, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Playlist), request.Id);
                }

                _context.Playlists.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
