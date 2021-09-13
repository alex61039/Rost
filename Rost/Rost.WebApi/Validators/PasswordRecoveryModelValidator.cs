using FluentValidation;
using Rost.Common;
using Rost.WebApi.Models.Account;

namespace Rost.WebApi.Validators
{
    public class PasswordRecoveryModelValidator : AbstractValidator<PasswordRecoveryModel>
    {
        public PasswordRecoveryModelValidator()
        {
            RuleFor(t => t).NotNull();
            RuleFor(t => t.Email).EmailAddress().WithMessage(ValidationErrorMessages.EmailIsEmptyOrInvalid);
        }
    }
}