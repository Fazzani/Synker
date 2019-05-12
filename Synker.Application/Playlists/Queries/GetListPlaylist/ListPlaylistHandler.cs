namespace Synker.Application.Playlists.Queries
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Synker.Application.Infrastructure.PagedResult;
    using Synker.Application.Interfaces;
    using Synker.Common;
    using Synker.Domain.Entities;
    using Synker.Domain.Entities.Core;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class ListPlaylistHandler : IRequestHandler<ListPlaylistQuery, PagedResult<PlaylistLookupViewModel>>
    {
        private readonly ISynkerDbContext _synkerDbContext;
        private readonly IMapper _mapper;

        public ListPlaylistHandler(ISynkerDbContext synkerDbContext, IMapper mapper)
        {
            _synkerDbContext = synkerDbContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<PlaylistLookupViewModel>> Handle(ListPlaylistQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Playlist, bool>> query = _ => true;
            Expression<Func<Playlist, bool>> expNameContains = e => e.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase);
            Expression<Func<Playlist, bool>> expCreatedAfter = e => e.CreatedDate >= request.CreatedFrom;
            Expression<Func<Playlist, bool>> expEnabled = e => request.Enabled == true ?
            e.State == OnlineState.Enabled :
            e.State == OnlineState.Disabled;

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.CombineWithAndAlso(expNameContains);
            }

            if (request.CreatedFrom.HasValue)
            {
                query = query.CombineWithAndAlso(expCreatedAfter);
            }

            if (request.Enabled.HasValue)
            {
                query = query.CombineWithAndAlso(expEnabled);
            }

            var pl = _synkerDbContext.Playlists.AsNoTracking().Where(query);

            return await pl
            .ProjectTo<PlaylistLookupViewModel>(_mapper.ConfigurationProvider)
            .GetPagedAsync(request.Page, request.PageSize, cancellationToken);
        }
    }
}
