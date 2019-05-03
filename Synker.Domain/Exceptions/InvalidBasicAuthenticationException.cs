using System;
using System.Collections.Generic;
using System.Text;

namespace Synker.Domain.Exceptions
{
    public class InvalidBasicAuthenticationException : Exception
    {
        public InvalidBasicAuthenticationException()
            : base("Invalid basic authentication, username must have a value")
        {

        }
    }
}