namespace Synker.Application.DataSources.Queries
{
    using FluentValidation;
    public class GetDataSourceMediasQueryValidator : AbstractValidator<GetDataSourceMediasQuery>
    {
        public GetDataSourceMediasQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(-1);
        }
    }
}
