namespace Synker.Domain.Entities
{
    using Synker.Domain.Entities.Core;
    using Synker.Domain.Exceptions;
    using Synker.Domain.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Media : EntityIdAudit<long>, IComparable<Media>, IComparable, IValidatableObject, IEquatable<Media>, IEqualityComparer<Media>
    {
        public Media()
        {
            Labels = new HashSet<Label>();
            Enabled = true;
            MediaType = MediaType.LiveTv;
        }

        public Media(List<Label> labels) : this()
        {
            labels.ForEach(x => this.AddNewLabel(x));
        }

        public UriAddress Url { get; set; }

        public string DisplayName { get; set; }

        public int Position { get; set; }

        public bool Enabled { get; set; }

        public Playlist Playlist { get; set; }

        public MediaType MediaType { get; set; }

        public ICollection<Label> Labels { get; private set; }

        public string Group => Labels.FirstOrDefault(x => x.Key == KnowedLabelKeys.GroupKey)?.Value;

        public Tvg Tvg { get; set; }

        #region Interfaces implementations

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is Media))
                throw new ArgumentException("Must be a media type");

            return Url.CompareTo(((Media)obj).Url);
        }

        public int CompareTo(Media other)
        {
            if (other == null)
                return 1;
            return Url.CompareTo(other.Url);
        }

        public static bool operator ==(Media m1, Media m2) => m1.Equals(m2);
        public static bool operator !=(Media m1, Media m2) => !m1.Equals(m2);
        public static bool operator >(Media m1, Media m2) => m1.Position > m2.Position;
        public static bool operator >=(Media m1, Media m2) => m1.Position >= m2.Position;
        public static bool operator <(Media m1, Media m2) => m1.Position < m2.Position;
        public static bool operator <=(Media m1, Media m2) => m1.Position <= m2.Position;

        public virtual bool Equals(Media other) => Url.Equals(other?.Url); //&& DisplayName.Equals(other.DisplayName);

        public override bool Equals(object obj)
        {
            if (!(obj is Media m))
                return false;
            return m.Url == Url; //&& m.DisplayName == DisplayName;
        }

        public override int GetHashCode() => Url?.GetHashCode() ?? 0;

        public override string ToString() => $"{Position} {DisplayName}";

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(DisplayName))
                yield return new ValidationResult("Media name is required");
            if (Url == null)
                yield return new ValidationResult("Media url is required");
        }

        public bool Equals(Media x, Media y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Url == y.Url && x.DisplayName == y.DisplayName)
                return true;
            return false;
        }

        public int GetHashCode(Media obj)
        {
            var code = obj.Url.GetHashCode() ^ obj.DisplayName.GetHashCode();
            return code.GetHashCode();
        }

        #endregion

        /// <summary>
        /// Add only knowned attributes
        /// avoid duplicated label
        /// avoid null key 
        /// </summary>
        public void AddNewLabel(Label label)
        {
            if (string.IsNullOrEmpty(label.Key))
            {
                throw new KeyLabelMediaException(this);
            }

            if (Labels.Any(l => l.Equals(label)))
            {
                throw new DuplicatedMediaLabelException(label, this);
            }

            Labels.Add(label);
        }

        public string Format(IFormatterVisitor mediaFormatter) => mediaFormatter.Visit(this);

        public static class KnowedLabelKeys
        {
            public const string GroupKey = "media_group";
            public const string Lang = "media_lang";
        }
    }

    public enum MediaType : Byte
    {
        LiveTv = 0,
        Radio,
        /// <summary>
        /// video file
        /// </summary>
        Video,
        /// <summary>
        /// audio file
        /// </summary>
        Audio,
        Other
    }
}
