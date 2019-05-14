namespace Synker.Application.PlaylistFormaters
{
    using Synker.Domain.Entities;
    using Synker.Domain.Infrastructure;
    using System;
    using System.Linq;
    using System.Text;

    public class M3UFormater : IFormatterVisitor
    {
        private readonly string _headerFile = "#EXTM3U";

        public string HeaderFile => _headerFile;

        public string Visit(Playlist playlist)
        {
            if(playlist.Medias?.Count > 0)
            {
                var sb = new StringBuilder(_headerFile);
                sb.Append(Environment.NewLine);

                var list = playlist.Medias.Select(x => sb.Append(x.Format(this))).ToList();
                if (list.Count > 0)
                    return sb.ToString();
            }

            return string.Empty;
        }

        

        public string Visit(Media media) =>
         $"#EXTINF:{(Byte)media.MediaType} tvg-id=\"{media.Tvg?.Id}\" tvg-logo=\"{media.Tvg?.Logo}\" tvg-name=\"{media.Tvg?.Name}\" audio-track=\"{media.Tvg?.Audio_track}\" tvg-shift=\"{media.Tvg?.Shift}\" aspect-ratio=\"{media.Tvg?.Aspect_ratio}\" group-title=\"{media.Group}\", {media.DisplayName.Trim()}{ Environment.NewLine}{media.Url}{ Environment.NewLine}";
    }
}
