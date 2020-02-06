namespace Synker.Domain.Exceptions
{
    using Synker.Domain.Entities;
    using System;

    public class MediaSomePositionException : Exception
    {
        public MediaSomePositionException(Media media, Playlist playlist)
            : base($"Media {media.DisplayName} with the some position \"{media.Position}\" already exist in playlist {playlist}.", null)
        {

        }
    }
}
