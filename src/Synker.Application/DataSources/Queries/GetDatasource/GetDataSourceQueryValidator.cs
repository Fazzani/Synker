namespace Synker.Application.DataSources.Queries.GetDatasource
{
    using FluentValidation;
    public class GetDataSourceQueryValidator : AbstractValidator<GetDataSourceQuery>
    {
        public GetDataSourceQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(-1);
        }
    }
}
