namespace Synker.Application.Playlists.Queries
{
    using FluentValidation;
    public class GetPlaylistQueryValidator : AbstractValidator<GetPlaylistQuery>
    {
        public GetPlaylistQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(-1);
        }
    }
}
