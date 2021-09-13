using FluentValidation;
using Rost.Common;
using Rost.WebApi.Models.Child;

namespace Rost.WebApi.Validators
{
    public class ChildAddModelValidator : AbstractValidator<ChildEditModel>
    {
        public ChildAddModelValidator()
        {
            RuleFor(t => t).NotNull();
            RuleFor(t => t.Sex).IsInEnum().WithMessage(ValidationErrorMessages.SexIsEmptyOrInvalid);
            RuleFor(t => t.Name).NotEmpty().WithMessage(ValidationErrorMessages.NameIsNullOrEmpty);
            RuleFor(t => t.BirthDay).NotNull().NotEmpty().WithMessage(ValidationErrorMessages.BirthdateIsNullOrEmpty);
        }
    }
}