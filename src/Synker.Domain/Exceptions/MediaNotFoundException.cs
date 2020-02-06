namespace Synker.Domain.Exceptions
{
    using Synker.Domain.Entities;
    using System;
    public class MediaNotFoundException : Exception
    {
        public MediaNotFoundException(long mediaId, Playlist playlist)
            : base($"\"{mediaId}\" not exist in \"{playlist.Name}\".", null)
        {

        }
    }
}
