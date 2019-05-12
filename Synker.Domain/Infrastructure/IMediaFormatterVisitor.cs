namespace Synker.Domain.Infrastructure
{
    using Synker.Domain.Entities;
    public interface IMediaFormatterVisitor
    {
        string Visit(Media media);
    }
}
