using FluentValidation;

namespace Synker.Application.DataSources.Commands.Update
{
    public class UpdateDataSourceValidator : AbstractValidator<UpdateDataSourceCommand>
    {
        public UpdateDataSourceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        }
    }
}
