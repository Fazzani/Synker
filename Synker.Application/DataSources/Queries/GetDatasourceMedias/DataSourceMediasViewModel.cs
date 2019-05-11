namespace Synker.Application.DataSources.Queries
{
    using AutoMapper;
    using Synker.Application.Interfaces.Mapping;
    using Synker.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using Xtream.Client;

    public class DataSourceMediasViewModel : IHaveCustomMapping
    {
        public string Url { get; set; }

        public string DisplayName { get; set; }

        public int Position { get; set; }

        public string Picon { get; set; }

        public string Group { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Media, DataSourceMediasViewModel>().ForMember(x => x.Url, m => m.MapFrom(f => f.Url.Url));
            configuration.CreateMap<Media, DataSourceMediasViewModel>().ForMember(x => x.Picon, m => m.MapFrom(f => f.Tvg.Logo));
            configuration.CreateMap<Media, DataSourceMediasViewModel>().ForMember(x => x.Group, m => m.MapFrom(f => f.Labels.FirstOrDefault(x => x.Key.Equals(Media.KnowedLabelKeys.GroupKey)).Value));
        }
    }

    //public class DataSourceMedia : IHaveCustomMapping
    //{
    //    public int Position { get; set; }
    //    public string Name { get; set; }
    //    public string Stream_type { get; set; }
    //    public string Type_name { get; set; }
    //    public int StreamId { get; set; }
    //    public string Stream_icon { get; set; }
    //    public string Epg_channel_id { get; set; }
    //    public string Added { get; set; }
    //    public string Category { get; set; }
    //    public int CategoryId { get; set; }
    //    public string Series_no { get; set; }
    //    public string Live { get; set; }

    //    public void CreateMappings(Profile configuration)
    //    {
    //        configuration.CreateMap<Channels, DataSourceMedia>()
    //             .ForMember(x => x.Position, p => p.MapFrom(f => f.Num));
    //        configuration.CreateMap<Channels, DataSourceMedia>()
    //             .ForMember(x => x.Category, p => p.MapFrom(f => f.Category_name));
    //    }
    //}
}