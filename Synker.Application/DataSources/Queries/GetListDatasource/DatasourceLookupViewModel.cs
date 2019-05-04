namespace Synker.Application.DataSources.Queries.GetListDatasource
{
    using AutoMapper;
    using Synker.Application.Interfaces.Mapping;
    using Synker.Domain.Entities;
    using System;
    using System.Linq.Expressions;

    public class DatasourceLookupViewModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public PlaylistDataSourceFormatEnum PlaylistDataSourceFormat { get; set; }

        public bool Enabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PlaylistDataSource, DatasourceLookupViewModel>()
                  .ForMember(x => x.Enabled, opt => opt.MapFrom(m => m.State == Domain.Entities.Core.OnlineState.Enabled));
        }

        public static Expression<Func<PlaylistDataSource, DatasourceLookupViewModel>> Projection
        {
            get
            {
                return ds => new DatasourceLookupViewModel
                {
                    Id = ds.Id,
                    Name = ds.Name,
                    Enabled = ds.State == Domain.Entities.Core.OnlineState.Enabled,
                    PlaylistDataSourceFormat = ds.PlaylistDataSourceFormat
                };
            }
        }

        public static DatasourceLookupViewModel Create(PlaylistDataSource ds)
        {
            return Projection.Compile().Invoke(ds);
        }
    }
}
