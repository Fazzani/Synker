namespace Synker.Application.PlaylistFormaters
{
    using Synker.Application.Interfaces;
    using Synker.Domain.Infrastructure;
    public class DefaultFormatterFactory : IFormatterFactory
    {
        public IFormatterVisitor Create(string formatter = "m3u")
        {
            switch (formatter)
            {
                case "tvlist":
                    return new TvListFormater();
                default:
                    return new M3UFormater();
            }
        }
    }
}
