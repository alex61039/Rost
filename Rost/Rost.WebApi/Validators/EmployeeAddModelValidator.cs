using FluentValidation;
using Rost.Common;
using Rost.WebApi.Models.Institution;

namespace Rost.WebApi.Validators
{
    public class EmployeeAddModelValidator : AbstractValidator<EmployeeAddModel>
    {
        public EmployeeAddModelValidator()
        {
            RuleFor(t => t).NotNull();
            RuleFor(t => t.InstitutionId).NotEmpty().GreaterThan(0)
                .WithMessage(ValidationErrorMessages.FieldIsRequired("Иденитификатор"));
            RuleFor(t => t.User).NotEmpty().WithMessage(ValidationErrorMessages.FieldIsRequired("Номер или email пользователя"));
        }
    }
}