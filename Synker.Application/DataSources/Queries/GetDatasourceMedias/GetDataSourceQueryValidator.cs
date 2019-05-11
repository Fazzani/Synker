namespace Synker.Application.DataSources.Queries
{
    using FluentValidation;
    public class GetDataSourceMediasQueryValidator : AbstractValidator<DataSourceMediasQuery>
    {
        public GetDataSourceMediasQueryValidator()
        {
            RuleFor(x => x.DataSourceId).GreaterThan(-1);
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
