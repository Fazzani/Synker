namespace Synker.Application.Playlists.Commands
{
    using FluentValidation;

    public class CreatePlaylistValidator : AbstractValidator<CreatePlaylistCommand>
    {
        public CreatePlaylistValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.UserId).GreaterThanOrEqualTo(0);
            //TODO: Media validation
        }
    }
}
