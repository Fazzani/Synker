namespace Synker.Domain.Exceptions
{
    using Synker.Domain.Entities;
    using System;

    public class DuplicatedPlaylistMediaException : Exception
    {
        public DuplicatedPlaylistMediaException(Media media, Playlist playlist)
            : base($"\"{media?.ToString()}\" already exist in \"{playlist.Name}\".", null)
        {

        }
    }
}
