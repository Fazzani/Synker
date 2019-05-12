namespace Synker.Application.Playlists.Queries
{
    using AutoMapper;
    using Synker.Application.Interfaces.Mapping;
    using Synker.Domain.Entities;
    using Synker.Domain.Entities.Core;
    using System;
    using System.Linq.Expressions;

    public class PlaylistLookupViewModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public string Name { get; set; }


        public bool Enabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Playlist, PlaylistLookupViewModel>()
                  .ForMember(x => x.Enabled, opt => opt.MapFrom(m => m.State == OnlineState.Enabled));
        }

        public static Expression<Func<Playlist, PlaylistLookupViewModel>> Projection
        {
            get
            {
                return ds => new PlaylistLookupViewModel
                {
                    Id = ds.Id,
                    Name = ds.Name,
                    Enabled = ds.State == OnlineState.Enabled
                };
            }
        }

        public static PlaylistLookupViewModel Create(Playlist ds)
        {
            return Projection.Compile().Invoke(ds);
        }
    }
}
