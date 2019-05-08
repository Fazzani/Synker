namespace Synker.Application.DataSources.Queries
{
    using AutoMapper;
    using MediatR;
    using Synker.Application.Exceptions;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Get Datasource with medias
    /// </summary>
    public class GetDataSourceMediasQuery : IRequest<DataSourceMediasViewModel>
    {
        public long Id { get; set; }

        public class GetDataSourceMediasQueryHandler : IRequestHandler<GetDataSourceMediasQuery, DataSourceMediasViewModel>
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

            public async Task<DataSourceMediasViewModel> Handle(GetDataSourceMediasQuery request, CancellationToken cancellationToken)
            {
                var datasource = await _synkerDbContext.PlaylistDataSources.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                if (datasource == null)
                {
                    throw new NotFoundException(nameof(PlaylistDataSource), request.Id);
                }

                var dsViewModel = _mapper.Map<DataSourceMediasViewModel>(datasource);
                var dataSourceReader = _dataSourceReaderFactory.Create(datasource);

                if (dataSourceReader == null)
                {
                    throw new Exception($"Unable to resolve DataSource reader for {datasource.GetType().Name}");
                }

                dsViewModel.Medias = dataSourceReader.GetMedias();
                return dsViewModel;
            }
        }
    }
}