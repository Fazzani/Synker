namespace Synker.Application.DataSources.Queries.GetDatasource
{
    using AutoMapper;
    using Synker.Application.Interfaces.Mapping;
    using Synker.Domain.Entities;

    public class DataSourceViewModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public PlaylistDataSourceFormatEnum PlaylistDataSourceFormat { get; set; }

        public bool Enabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PlaylistDataSource, DataSourceViewModel>()
                  .ForMember(x => x.Enabled, opt => opt.MapFrom(m => m.State == Domain.Entities.Core.OnlineState.Enabled));
        }
    }
}