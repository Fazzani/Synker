using System;

namespace Synker.Application.Exceptions
{
    [Serializable]
    public class XtreamConnectionInfoException : Exception
    {
        public XtreamConnectionInfoException(string message) : base(message)
        {
        }
    }
}
