namespace Synker.Application.DataSources.Queries
{
    using AutoMapper;
    using MediatR;
    using Synker.Application.Exceptions;
    using Synker.Application.Infrastructure.PagedResult;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationException = Exceptions.ApplicationException;

    /// <summary>
    /// Get Datasource with medias
    /// </summary>
    public class DataSourceMediasQuery : IRequest<PagedResult<DataSourceMediasViewModel>>, IPagedRequest
    {
        public long DataSourceId { get; set; }

        public string MediaNameFilter { get; set; }

        public bool? MediasEnabledOnly { get; set; }

        public DateTime? MediasCreatedFrom { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public class GetDataSourceMediasQueryHandler : IRequestHandler<DataSourceMediasQuery, PagedResult<DataSourceMediasViewModel>>
        {
            private readonly ISynkerDbContext _synkerDbContext;
            private readonly IMapper _mapper;
            private readonly IDataSourceReaderFactory _dataSourceReaderFactory;

            public GetDataSourceMediasQueryHandler(ISynkerDbContext synkerDbContext, IMapper mapper, IDataSourceReaderFactory dataSourceReaderFactory)
            {
                _synkerDbContext = synkerDbContext;
                _mapper = mapper;
                _dataSourceReaderFactory = dataSourceReaderFactory;
            }

            public async Task<PagedResult<DataSourceMediasViewModel>> Handle(DataSourceMediasQuery request, CancellationToken cancellationToken)
            {
                var datasource = await _synkerDbContext.PlaylistDataSources.FindAsync(new object[] { request.DataSourceId }, cancellationToken: cancellationToken);

                if (datasource == null)
                {
                    throw new NotFoundException(nameof(PlaylistDataSource), request.DataSourceId);
                }

                var dataSourceReader = _dataSourceReaderFactory.Create(datasource);

                if (dataSourceReader == null)
                {
                    throw new ApplicationException($"Unable to resolve DataSource reader for {datasource.GetType().Name}");
                }

               return dataSourceReader.GetMedias().GetPaged(request.Page, request.PageSize, _mapper.Map<DataSourceMediasViewModel>);
            }
        }
    }
}