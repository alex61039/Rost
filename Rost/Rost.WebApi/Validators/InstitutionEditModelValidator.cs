using FluentValidation;
using Rost.Common;
using Rost.WebApi.Models.Institution;

namespace Rost.WebApi.Validators
{
    public class InstitutionEditModelValidator : AbstractValidator<InstitutionEditModel>
    {
        public InstitutionEditModelValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.StructureId).NotEmpty().WithMessage(ValidationErrorMessages.FieldIsRequired("Структура"));
            RuleFor(x => x.Address).NotEmpty().WithMessage(ValidationErrorMessages.FieldIsRequired("Адрес"));
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationErrorMessages.FieldIsRequired("Название"));
            RuleFor(x => x.Phone).NotEmpty().WithMessage(ValidationErrorMessages.FieldIsRequired("Телефон"));
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationErrorMessages.FieldIsRequired("Email"));
            RuleFor(x => x.CityId).NotEmpty().GreaterThan(0).WithMessage(ValidationErrorMessages.FieldIsRequired("Город"));
            RuleFor(x => x.DistrictId).NotEmpty().GreaterThan(0).WithMessage(ValidationErrorMessages.FieldIsRequired("Район"));
            RuleFor(x => x.MunicipalUnionId).NotEmpty().GreaterThan(0).WithMessage(ValidationErrorMessages.FieldIsRequired("Муниципальное объединение"));
            
            RuleFor(x => x.Name).MaximumLength(200)
                .WithMessage(ValidationErrorMessages.FieldShouldBeLessThan("Название", 200));
            RuleFor(x => x.Description).MaximumLength(200)
                .WithMessage(ValidationErrorMessages.FieldShouldBeLessThan("Описание", 200));
        }
    }
}