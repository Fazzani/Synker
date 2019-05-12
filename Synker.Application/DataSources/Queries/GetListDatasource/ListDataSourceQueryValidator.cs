namespace Synker.Application.DataSources.Queries.GetListDatasource
{
    using FluentValidation;
    using System;
    public class ListDataSourceQueryValidator : AbstractValidator<ListDatasourceQuery>
    {
        public ListDataSourceQueryValidator()
        {
            RuleFor(x => x.CreatedFrom).LessThanOrEqualTo(DateTime.UtcNow).When(x => x.CreatedFrom.HasValue);
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
