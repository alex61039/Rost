using FluentValidation;
using Rost.Common;
using Rost.WebApi.Models.Account;

namespace Rost.WebApi.Validators
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidator()
        {
            RuleFor(t => t).NotNull();
            RuleFor(t => t.OldPassword).NotEmpty().WithMessage(ValidationErrorMessages.PasswordOldIsNullOrEmpty);
            RuleFor(t => t.Password).NotEmpty().WithMessage(ValidationErrorMessages.PasswordNewIsNullOrEmpty);
            RuleFor(t => t.ConfirmPassword).NotEmpty().WithMessage(ValidationErrorMessages.PasswordNewConfirmIsNullOrEmpty);
            RuleFor(t => t.Password).Equal(t => t.ConfirmPassword).WithMessage(ValidationErrorMessages.PasswordsIsNotEqual);
            RuleFor(t => t.ConfirmPassword).Equal(t => t.Password).WithMessage(ValidationErrorMessages.PasswordsIsNotEqual);
        }
    }
}