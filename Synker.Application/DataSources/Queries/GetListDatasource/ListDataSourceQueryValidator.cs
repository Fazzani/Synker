namespace Synker.Application.DataSources.Queries.GetListDatasource
{
    using FluentValidation;
    using System;
    public class ListDataSourceQueryValidator : AbstractValidator<ListDatasourceQuery>
    {
        public ListDataSourceQueryValidator()
        {
            RuleFor(x => x.CreatedFrom).LessThanOrEqualTo(DateTime.UtcNow).When(x => x.CreatedFrom.HasValue);
        }
    }
}
