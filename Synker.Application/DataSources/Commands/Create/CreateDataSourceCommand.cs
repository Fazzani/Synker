using MediatR;
using Synker.Application.Exceptions;
using Synker.Application.Interfaces;
using Synker.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Synker.Application.DataSources.Commands.Create
{
    public class CreateDataSourceCommand : IRequest
    {
        public string Name { get; set; }

        public bool State { get; set; }

        public long UserId { get; set; }

        public PlaylistDataSourceFormatEnum PlaylistDataSourceFormat { get; set; }

        public class Handler : IRequestHandler<CreateDataSourceCommand, Unit>
        {
            private readonly ISynkerDbContext _context;
            private readonly IMediator _mediator;

            public Handler(ISynkerDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CreateDataSourceCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken: cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(User), request.UserId);
                }

                var entity = new PlaylistDataSource
                {
                    Name = request.Name,
                    State = request.State ? Domain.Entities.Core.OnlineState.Enabled : Domain.Entities.Core.OnlineState.Disabled,
                    User = user,
                    PlaylistDataSourceFormat = request.PlaylistDataSourceFormat
                };

                _context.PlaylistDataSources.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new DataSourceCreated { DataSourceId = entity.Id }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
