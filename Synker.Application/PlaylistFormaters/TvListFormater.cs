namespace Synker.Application.PlaylistFormaters
{
    using Synker.Domain.Entities;
    using Synker.Domain.Infrastructure;
    using System;
    using System.Linq;
    using System.Text;

    public class TvListFormater : IFormatterVisitor
    {
        public string Visit(Playlist playlist)
        {
            var sb = new StringBuilder();
            if (playlist.Medias?.Count > 0)
            {
                var list = playlist.Medias.Select(x => sb.Append(x.Format(this))).ToList();
                if (list.Count > 0)
                    return sb.ToString();
            }
            
            return string.Empty;
        }

        public string Visit(Media media) =>
           $"{media.DisplayName.Trim()}{Environment.NewLine}{media.Url}{Environment.NewLine}";
    }
}
