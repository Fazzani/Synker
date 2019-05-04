namespace Synker.Application.DataSources.Queries.GetDatasource
{
    using MediatR;
    public class GetDataSourceQuery : IRequest<DataSourceViewModel>
    {
        public long Id { get; set; }
    }
}