namespace Synker.Domain.Exceptions
{
    using System;
    public class UriAddressFormatException : Exception
    {
        public UriAddressFormatException(string url, Exception ex)
            : base($"Url \"{url}\" is invalid.", ex)
        {

        }
    }
}
