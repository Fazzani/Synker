namespace Synker.Application.Interfaces
{
    using Synker.Domain.Infrastructure;
    public interface IFormatterFactory
    {
        IFormatterVisitor Create(string formatter = "m3u");
    }
}
