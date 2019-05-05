namespace Synker.Application.DataSources.Queries.GetListDatasource
{
    using MediatR;
    using Synker.Application.Infrastructure.PagedResult;
    using System;
    public class ListDatasourceQuery : IRequest<PagedResult<DatasourceLookupViewModel>>, IPagedRequest
    {
        public string Name { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public int Page { get; set; } = 0;

        public int PageSize { get; set; } = 10;
    }
}
