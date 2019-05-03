namespace Synker.Domain.Exceptions
{
    using Synker.Domain.Entities;
    using System;

    public class DuplicatedMediaLabelException : Exception
    {
        public DuplicatedMediaLabelException(Label label, Media media)
            : base($"\"{label?.ToString()}\" already exist in \"{media.DisplayName}\".", null)
        {

        }
    }
}
