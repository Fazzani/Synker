namespace Synker.Application.DataSources.Queries
{
    using AutoMapper;
    using Synker.Application.Interfaces.Mapping;
    using Synker.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;

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
}