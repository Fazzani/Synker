using Synker.Domain.Entities;
namespace Synker.Domain.Exceptions
{
    using System;
    public class KeyLabelMediaException : Exception
    {
        public KeyLabelMediaException(Media media)
            : base($"Label Key must has a value for \"{media?.ToString()}\".", null)
        {

        }
    }
}
