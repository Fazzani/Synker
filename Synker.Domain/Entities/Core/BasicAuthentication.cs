namespace Synker.Domain.Entities.Core
{
    using System;
    using Synker.Domain.Infrastructure;
    using System.Collections.Generic;
    public class BasicAuthentication : ValueObject
    {
        public string User { get; set; }
        public string Password { get; set; }

      
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return User;
            yield return Password;
        }
    }
}
