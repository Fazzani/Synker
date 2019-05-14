namespace Synker.Domain.Entities
{
    using Synker.Domain.Entities.Core;
    using Synker.Domain.Exceptions;
    using Synker.Domain.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Playlist : EntityIdAudit<long>, IAggregateRoot
    {
        public Playlist()
        {
            Medias = new HashSet<Media>();
        }

        public string Name { get; set; }

        public OnlineState State { get; set; }

        public User User { get; set; }

        public ICollection<Media> Medias { get; } //TODO: shadow property and pass it to private scope

        public void AddMedia(Media media)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(media, new ValidationContext(media), validationResults, true))
            {
                throw new ValidationException(string.Join(',', validationResults.Select(x => x.ErrorMessage)));
            }

            if (Medias.Any(x => x.Equals(media)))
            {
                throw new DuplicatedPlaylistMediaException(media, this);
            }

            if (media.Position == -1)
            {
                media.Position = Medias.Count > 0 ? Medias.Max(x => x.Position) + 1 : 0;
            }
            else
            {
                if (Medias.Any(x => x.Position == media.Position))
                {
                    throw new MediaSomePositionException(media, this);
                }
            }

            Medias.Add(media);
        }

        public bool TryAddMedia(Media media, out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(media, new ValidationContext(media), validationResults, true))
            {
                return false;
            }

            if (Medias.Any(x => x.Equals(media)))
            {
                validationResults.Add(new ValidationResult(new DuplicatedPlaylistMediaException(media, this).Message));
                return false;
            }

            if (media.Position == -1)
            {
                media.Position = Medias.Count > 0 ? Medias.Max(x => x.Position) + 1 : 0;
            }
            else
            {
                if (Medias.Any(x => x.Position == media.Position))
                {
                    validationResults.Add(new ValidationResult(new MediaSomePositionException(media, this).Message));
                    return false;
                }
            }

            media.Playlist = this;
            Medias.Add(media);
            return true;
        }

        public void AddRangeMedia(List<Media> medias)
        {
            if (medias != null)
            {
                medias.ForEach(AddMedia);
            }
        }

        public bool RemoveMedia(long id)
        {
            if (!Medias.Any(x => x.Id == id))
            {
                throw new MediaNotFoundException(id, this);
            }

            return Medias.Remove(Medias.First(x => x.Id == id));
        }
    }
}
