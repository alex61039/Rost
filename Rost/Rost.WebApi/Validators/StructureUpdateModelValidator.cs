using FluentValidation;
using Rost.Common;
using Rost.WebApi.Models.Structure;

namespace Rost.WebApi.Validators
{
    public class StructureUpdateModelValidator : AbstractValidator<StructureUpdateModel>
    {
        public StructureUpdateModelValidator()
        {
            RuleFor(t => t).NotNull();
            RuleFor(t => t.Id).NotEmpty().GreaterThan(0);
            RuleFor(t => t.Name).NotEmpty().WithMessage(ValidationErrorMessages.FieldIsRequired("Название"));
        }
    }
}