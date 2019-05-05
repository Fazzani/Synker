namespace Synker.Application.DataSources.Commands.Create
{
    using FluentValidation;

    public class CreateDataSourceValidator : AbstractValidator<CreateDataSourceCommand>
    {
        public CreateDataSourceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.UserId).GreaterThanOrEqualTo(0);
        }
    }
}
