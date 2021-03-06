﻿namespace Synker.Application.DataSources.Queries.GetDatasource
{
    using MediatR;
    using AutoMapper;
    using Synker.Application.Exceptions;
    using Synker.Application.Interfaces;
    using Synker.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    public class GetDataSourceQuery : IRequest<DataSourceViewModel>
    {
        public long Id { get; set; }

        public class GetDataSourceQueryHandler : IRequestHandler<GetDataSourceQuery, DataSourceViewModel>
        {
            private readonly ISynkerDbContext _synkerDbContext;
            private readonly IMapper _mapper;

            public GetDataSourceQueryHandler(ISynkerDbContext synkerDbContext, IMapper mapper)
            {
                _synkerDbContext = synkerDbContext;
                _mapper = mapper;
            }

            public async Task<DataSourceViewModel> Handle(GetDataSourceQuery request, CancellationToken cancellationToken)
            {
                var datasource = await _synkerDbContext.PlaylistDataSources.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);

                if (datasource == null)
                {
                    throw new NotFoundException(nameof(PlaylistDataSource), request.Id);
                }

                return _mapper.Map<DataSourceViewModel>(datasource);
            }
        }
    }
}