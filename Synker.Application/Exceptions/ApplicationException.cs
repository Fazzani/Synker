using System;

namespace Synker.Application.Exceptions
{
    [Serializable]
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message)
        {

        }
    }
}
