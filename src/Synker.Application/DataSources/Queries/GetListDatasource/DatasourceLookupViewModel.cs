namespace Synker.Application.DataSources.Queries.GetListDatasource
{
    using AutoMapper;
    using Synker.Application.Interfaces.Mapping;
    using Synker.Domain.Entities;
    using Synker.Domain.Entities.Core;
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
                  .ForMember(x => x.Enabled, opt => opt.MapFrom(m => m.State == OnlineState.Enabled));
        }

        public static Expression<Func<PlaylistDataSource, DatasourceLookupViewModel>> Projection
        {
            get
            {
                return ds => new DatasourceLookupViewModel
                {
                    Id = ds.Id,
                    Name = ds.Name,
                    Enabled = ds.State == OnlineState.Enabled,
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
