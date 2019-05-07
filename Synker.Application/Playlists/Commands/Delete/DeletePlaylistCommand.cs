namespace Synker.Application.Playlists.Commands.Delete
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
            private readonly IMediator _mediator;

            public Handler(ISynkerDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Playlists.FindAsync(new object[] { request.Id }, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Playlist), entity.Id);
                }

                _context.Playlists.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
