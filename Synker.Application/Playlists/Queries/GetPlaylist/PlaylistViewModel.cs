namespace Synker.Application.Playlists.Queries
{
    using AutoMapper;
    using Synker.Application.Interfaces.Mapping;
    using Synker.Domain.Entities;
    using System;
    using System.Collections.Generic;

    public class PlaylistViewModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public User User { get; set; }

        public List<Media> Medias { get; }

        public DateTime UpdatedDate { get;  }

        public DateTime CreatedDate { get;  }

        public bool Enabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Playlist, PlaylistViewModel>()
                  .ForMember(x => x.Enabled, opt => opt.MapFrom(m => m.State == Domain.Entities.Core.OnlineState.Enabled));
        }
    }
}