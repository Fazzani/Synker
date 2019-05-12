namespace Synker.Application.Playlists.Queries
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Synker.Application.Exceptions;
    using Synker.Application.Infrastructure.PagedResult;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Get Datasource with medias
    /// </summary>
    public class PlaylistMediasQuery : IRequest<PagedResult<PlaylistMediasViewModel>>, IPagedRequest
    {
        public long Id { get; set; }

        public string MediaNameFilter { get; set; }

        public bool? MediasEnabledOnly { get; set; }

        public DateTime? MediasCreatedFrom { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public class GetDataSourceMediasQueryHandler : IRequestHandler<PlaylistMediasQuery, PagedResult<PlaylistMediasViewModel>>
        {
            private readonly ISynkerDbContext _synkerDbContext;
            private readonly IMapper _mapper;

            public GetDataSourceMediasQueryHandler(ISynkerDbContext synkerDbContext, IMapper mapper)
            {
                _synkerDbContext = synkerDbContext;
                _mapper = mapper;
            }

            public async Task<PagedResult<PlaylistMediasViewModel>> Handle(PlaylistMediasQuery request, CancellationToken cancellationToken)
            {
                var playlist = await _synkerDbContext.Playlists
                    .Include(x => x.Medias)
                    .FirstOrDefaultAsync(x=>x.Id == request.Id, cancellationToken);

                if (playlist == null)
                {
                    throw new NotFoundException(nameof(Playlist), request.Id);
                }

                return playlist.Medias.GetPaged(request.Page, request.PageSize, _mapper.Map<PlaylistMediasViewModel>);
            }
        }
    }
}