using MediatR;
using Synker.Application.Exceptions;
using Synker.Application.Interfaces;
using Synker.Domain.Entities;
using Synker.Domain.Entities.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Application.DataSources.Commands.Update
{
    public class UpdateDataSourceCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public bool State { get; set; }

        public PlaylistDataSourceFormatEnum PlaylistDataSourceFormat { get; set; }

        public class Handler : IRequestHandler<UpdateDataSourceCommand, Unit>
        {
            private readonly ISynkerDbContext _context;

            public Handler(ISynkerDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateDataSourceCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.PlaylistDataSources.FindAsync(new object[] { request.Id }, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(PlaylistDataSource), request.Id);
                }

                entity.Name = request.Name;
                entity.PlaylistDataSourceFormat = request.PlaylistDataSourceFormat;
                entity.State = request.State ? OnlineState.Enabled : OnlineState.Disabled;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
