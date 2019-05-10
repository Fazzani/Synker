namespace Synker.Domain.Entities.Core
{
    using Synker.Domain.Exceptions;
    using Synker.Domain.Infrastructure;
    using System;
    using System.Collections.Generic;

    public sealed class UriAddress : ValueObject, IComparable<UriAddress>
    {
        private UriAddress()
        {
        }

        public string Url
        {
            get { return Uri?.AbsoluteUri; }
            private set { Uri = new Uri(value); }
        }

        public static UriAddress For(string url)
        {
            var uriAddress = new UriAddress();

            try
            {
                uriAddress.Uri = new Uri(url);
            }
            catch (Exception ex)
            {
                throw new UriAddressFormatException(url, ex);
            }

            return uriAddress;
        }

        public static bool TryFor(string url, out UriAddress uriAddress)
        {
            uriAddress = new UriAddress();

            try
            {
                uriAddress.Uri = new Uri(url);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public Uri Uri { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Uri;
        }

        public int CompareTo(UriAddress other)
        {
            if (other == null)
                return 1;
            return Uri.ToString().CompareTo(other.Uri.ToString());
        }

        public override string ToString() => Url;
    }
}
