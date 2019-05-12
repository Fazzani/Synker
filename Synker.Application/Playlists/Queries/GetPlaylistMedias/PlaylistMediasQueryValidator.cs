namespace Synker.Application.Playlists.Queries
{
    using FluentValidation;
    public class PlaylistMediasQueryValidator : AbstractValidator<PlaylistMediasQuery>
    {
        public PlaylistMediasQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(-1);
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
