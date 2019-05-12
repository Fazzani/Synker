namespace Synker.Application.Playlists.Commands
{
    using FluentValidation;
    public class UpdatePlaylistValidator : AbstractValidator<UpdatePlaylistCommand>
    {
        public UpdatePlaylistValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        }
    }
}
