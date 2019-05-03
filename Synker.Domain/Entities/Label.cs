using System;
using System.Collections.Generic;

namespace Synker.Domain.Entities
{
    public class Label : IComparable<Label>, IEquatable<Label>, IEqualityComparer<Label>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public int CompareTo(Label other)
        {
            if (other == null)
                return 1;
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        public bool Equals(Label x, Label y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Key == y.Key && x.Value == y.Value)
                return true;
            return false;
        }

        public bool Equals(Label other)
        {
            if (!(other is Label m))
                return false;
            return m.Key == Key && m.Value == Value;
        }

        public override int GetHashCode() => Key.GetHashCode() ^ Value.GetHashCode();

        public int GetHashCode(Label obj) => obj.GetHashCode();

        public override string ToString() => $"{Key}: {Value}";
    }
}