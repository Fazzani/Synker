namespace Synker.Application.Playlists.Queries
{
    using AutoMapper;
    using MediatR;
    using Synker.Application.Exceptions;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetPlaylistQuery : IRequest<PlaylistViewModel>
    {
        public long Id { get; set; }

        public class GetPlaylistQueryHandler : IRequestHandler<GetPlaylistQuery, PlaylistViewModel>
        {
            private readonly ISynkerDbContext _synkerDbContext;
            private readonly IMapper _mapper;

            public GetPlaylistQueryHandler(ISynkerDbContext synkerDbContext, IMapper mapper)
            {
                _synkerDbContext = synkerDbContext;
                _mapper = mapper;
            }

            public async Task<PlaylistViewModel> Handle(GetPlaylistQuery request, CancellationToken cancellationToken)
            {
                var playlist = await _synkerDbContext.Playlists.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                if (playlist == null)
                {
                    throw new NotFoundException(nameof(Playlist), request.Id);
                }

                return _mapper.Map<PlaylistViewModel>(playlist);
            }
        }
    }
}