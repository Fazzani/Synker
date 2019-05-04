namespace Synker.Application.DataSources.Queries.GetListDatasource
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Synker.Application.Interfaces;
    using Synker.Common;
    using Synker.Domain.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class ListDatasourceHandler : IRequestHandler<ListDatasourceQuery, ListDatasourceViewModel>
    {
        private readonly ISynkerDbContext _synkerDbContext;
        private readonly IMapper _mapper;

        public ListDatasourceHandler(ISynkerDbContext synkerDbContext, IMapper mapper)
        {
            _synkerDbContext = synkerDbContext;
            _mapper = mapper;
        }

        public async Task<ListDatasourceViewModel> Handle(ListDatasourceQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<PlaylistDataSource, bool>> query = _ => true;
            Expression<Func<PlaylistDataSource, bool>> expNameContains = e => e.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase);
            Expression<Func<PlaylistDataSource, bool>> expCreatedAfter = e => e.CreatedDate >= request.CreatedFrom;
            Expression<Func<PlaylistDataSource, bool>> expEnabled = e => request.Enabled == true ? e.State == Domain.Entities.Core.OnlineState.Enabled : e.State == Domain.Entities.Core.OnlineState.Disabled;

            if (!string.IsNullOrEmpty(request.Name))
                query = query.CombineWithAndAlso(expNameContains);

            if (request.CreatedFrom.HasValue)
                query = query.CombineWithAndAlso(expCreatedAfter);

            if (request.Enabled.HasValue)
                query = query.CombineWithAndAlso(expEnabled);

            var ds = _synkerDbContext.PlaylistDataSources.Where(query);

            var model = new ListDatasourceViewModel();

            model.Items = await ds
                .ProjectTo<DatasourceLookupViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return model;
        }
    }
}
