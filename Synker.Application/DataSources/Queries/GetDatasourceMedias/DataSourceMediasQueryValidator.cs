namespace Synker.Application.DataSources.Queries
{
    using FluentValidation;
    public class DataSourceMediasQueryValidator : AbstractValidator<DataSourceMediasQuery>
    {
        public DataSourceMediasQueryValidator()
        {
            RuleFor(x => x.DataSourceId).GreaterThan(-1);
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
