namespace Synker.Application.DataSources.Commands.Create
{
    using FluentValidation;

    public class CreateDataSourceValidator : AbstractValidator<CreateDataSourceCommand>
    {
        public CreateDataSourceValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UserId).GreaterThanOrEqualTo(0);
        }
    }
}
