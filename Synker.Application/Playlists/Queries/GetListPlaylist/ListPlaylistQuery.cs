namespace Synker.Application.Playlists.Queries
{
    using MediatR;
    using Synker.Application.Infrastructure.PagedResult;
    using System;
    public class ListPlaylistQuery : IRequest<PagedResult<PlaylistLookupViewModel>>, IPagedRequest
    {
        public string Name { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
