namespace Synker.Domain.Infrastructure
{
    using Synker.Domain.Entities;
    public interface IFormatterVisitor
    {
        string Visit(Media media);
        string Visit(Playlist playlist);
    }
}
