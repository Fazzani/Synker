namespace Synker.Application.DataSources.Queries
{
    using MediatR;
    public class GetDataSourceQuery : IRequest<DataSourceViewModel>
    {
        public long Id { get; set; }
    }
}