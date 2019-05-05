using MediatR;
using Synker.Application.Exceptions;
using Synker.Application.Interfaces;
using Synker.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Application.DataSources.Commands.Delete
{
    public class DeleteDataSourceCommand : IRequest
    {
        public long Id { get; set; }

        public class Handler : IRequestHandler<DeleteDataSourceCommand, Unit>
        {
            private readonly ISynkerDbContext _context;
            private readonly IMediator _mediator;

            public Handler(ISynkerDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(DeleteDataSourceCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.PlaylistDataSources.FindAsync(new object[] { request.Id }, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(PlaylistDataSource), entity.Id);
                }

                _context.PlaylistDataSources.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
