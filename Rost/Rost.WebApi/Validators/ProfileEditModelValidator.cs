using FluentValidation;
using Rost.Common;
using Rost.WebApi.Models.Profile;

namespace Rost.WebApi.Validators
{
    public class ProfileEditModelValidator : AbstractValidator<ProfileEditModel>
    {
        public ProfileEditModelValidator()
        {
            RuleFor(t => t).NotNull();
            RuleFor(t => t.Email).NotEmpty().WithMessage(ValidationErrorMessages.EmailIsEmptyOrInvalid);
            RuleFor(t => t.Name).NotEmpty().WithMessage(ValidationErrorMessages.NameIsNullOrEmpty);
            RuleFor(t => t.Surname).NotEmpty().WithMessage(ValidationErrorMessages.SurnameIsNullOrEmpty);
            RuleFor(t => t.Phone).NotEmpty().WithMessage(ValidationErrorMessages.PhoneIsNullOrEmpty);
            RuleFor(t => t.CityId).NotEmpty().GreaterThan(0).WithMessage(ValidationErrorMessages.CityIsNullOrEmpty);
            RuleFor(t => t.DistrictId).NotEmpty().GreaterThan(0).WithMessage(ValidationErrorMessages.DistrictIsNullOrEmpty);
            RuleFor(t => t.MunicipalUnionId).NotEmpty().GreaterThan(0).WithMessage(ValidationErrorMessages.MunicipalIsNullOrEmpty);
        }
    }
}