namespace Synker.Application.DataSources.Queries
{
    using FluentValidation;
    public class GetDataSourceMediasQueryValidator : AbstractValidator<DataSourceMediasQuery>
    {
        public GetDataSourceMediasQueryValidator()
        {
            RuleFor(x => x.DataSourceId).GreaterThan(-1);
        }
    }
}
