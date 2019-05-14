namespace Synker.Application.Playlists.Queries
{
    using FluentValidation;
    public class PlaylistFileQueryValidator : AbstractValidator<PlaylistFileQuery>
    {
        public PlaylistFileQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(-1);
            RuleFor(x => x.FileFormat).NotEmpty();
        }
    }
}
