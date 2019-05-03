namespace Synker.Domain.Entities
{
    using Synker.Domain.Entities.Core;
    using Synker.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Media : EntityIdAudit<long>, IComparable<Media>, IComparable, IValidatableObject, IEquatable<Media>, IEqualityComparer<Media>
    {
        public Media()
        {
            Labels = new HashSet<Label>();
        }

        public UriAddress Url { get; set; }

        public string DisplayName { get; set; }

        public int Position { get; set; }

        public Playlist Playlist { get; set; }

        public ICollection<Label> Labels { get; private set; }

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

        public bool Equals(Media other) => Url.Equals(other.Url); //&& DisplayName.Equals(other.DisplayName);

        public override bool Equals(object obj)
        {
            if (!(obj is Media m))
                return false;
            return m.Url == Url; //&& m.DisplayName == DisplayName;
        }

        public override int GetHashCode() => Url.GetHashCode();

        public override string ToString() => $"{Position} {DisplayName}";

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(DisplayName))
                yield return new ValidationResult("Media name is required");
            if (Url == null)
                yield return new ValidationResult("Media url is required");
        }

        //public virtual string Format(IMediaFormatterVisitor mediaFormatter) => mediaFormatter.Visit(this);

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
        /// avoid duplicated
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
    }
}
