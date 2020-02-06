namespace Synker.Domain.Entities.Core
{
    using Synker.Domain.Exceptions;
    using Synker.Domain.Infrastructure;
    using System.Collections.Generic;

    public class BasicAuthentication : ValueObject
    {
        private BasicAuthentication()
        {

        }

        public BasicAuthentication(string user, string password)
        {
            User = user;
            Password = password;
        }

        public static BasicAuthentication For(string user, string password)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new InvalidBasicAuthenticationException();
            }
            return new BasicAuthentication(user, password);
        }

        public string User { get; private set; }
        public string Password { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return User;
            yield return Password;
        }
    }
}
