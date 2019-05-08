namespace Synker.Application.DataSources.Queries
{
    using AutoMapper;
    using Synker.Application.Interfaces.Mapping;
    using Synker.Domain.Entities;
    using System.Collections.Generic;
    using Xtream.Client;

    public class DataSourceMediasViewModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public PlaylistDataSourceFormatEnum PlaylistDataSourceFormat { get; set; }

        public List<Media> Medias { get; set; }

        public bool Enabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<PlaylistDataSource, DataSourceMediasViewModel>()
                  .ForMember(x => x.Enabled, opt => opt.MapFrom(m => m.State == Domain.Entities.Core.OnlineState.Enabled));
        }
    }

    public class DataSourceMedia : IHaveCustomMapping
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public string Stream_type { get; set; }
        public string Type_name { get; set; }
        public int StreamId { get; set; }
        public string Stream_icon { get; set; }
        public string Epg_channel_id { get; set; }
        public string Added { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string Series_no { get; set; }
        public string Live { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Channels, DataSourceMedia>()
                 .ForMember(x => x.Position, p => p.MapFrom(f => f.Num));
            configuration.CreateMap<Channels, DataSourceMedia>()
                 .ForMember(x => x.Category, p => p.MapFrom(f => f.Category_name));
        }
    }
}