using FluentValidation;
using Rost.Common;
using Rost.WebApi.Models.Account;

namespace Rost.WebApi.Validators
{
    public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordModelValidator()
        {
            RuleFor(t => t).NotNull();
            RuleFor(t => t.UserId).NotNull().NotEmpty().WithMessage(ValidationErrorMessages.UserIdIsNullOrEmpty);
        }
    }
}