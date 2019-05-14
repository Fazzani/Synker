namespace Synker.Application.Playlists.Queries
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Synker.Application.Exceptions;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Get Datasource with medias
    /// </summary>
    public class PlaylistFileQuery : IRequest<string>
    {
        public long Id { get; set; }

        public string FileFormat { get; set; } = "m3u";

        public class PlaylistFileQueryQueryHandler : IRequestHandler<PlaylistFileQuery, string>
        {
            private readonly ISynkerDbContext _synkerDbContext;
            private readonly IFormatterFactory _formatterFactory;

            public PlaylistFileQueryQueryHandler(ISynkerDbContext synkerDbContext, IFormatterFactory formatterFactory)
            {
                _synkerDbContext = synkerDbContext;
                _formatterFactory = formatterFactory;
            }

            public async Task<string> Handle(PlaylistFileQuery request, CancellationToken cancellationToken)
            {
                var playlist = await _synkerDbContext.Playlists
                    .Include(x => x.Medias)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (playlist == null)
                {
                    throw new NotFoundException(nameof(Playlist), request.Id);
                }

                var playlistFormater = _formatterFactory.Create(request.FileFormat);
                return playlistFormater.Visit(playlist);
            }
        }
    }
}