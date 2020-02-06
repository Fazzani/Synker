namespace Synker.Application.Playlists.Queries
{
    using FluentValidation;
    using System;
    public class ListPlaylistQueryValidator : AbstractValidator<ListPlaylistQuery>
    {
        public ListPlaylistQueryValidator()
        {
            RuleFor(x => x.CreatedFrom).LessThanOrEqualTo(DateTime.UtcNow).When(x => x.CreatedFrom.HasValue);
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
