namespace Synker.Domain.Entities
{
    using Synker.Domain.Infrastructure;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Tvg : ValueObject
    {
        public string Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Shift { get; set; }
        public string Audio_track { get; set; }
        public string Aspect_ratio { get; set; }
        public string TvgIdentify { get; set; }
        public string TvgSiteSource { get; set; }

        public override string ToString() => $"{Id} : {Name} {Shift}";

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Name;
        }
    }
}
