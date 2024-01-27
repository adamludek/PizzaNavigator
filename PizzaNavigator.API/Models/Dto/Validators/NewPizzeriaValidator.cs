using FluentValidation;

namespace PizzaNavigator.API.Models.Dto.Validators
{
    public class NewPizzeriaValidator : AbstractValidator<NewPizzeriaDto>
    {
        public NewPizzeriaValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.Street).NotEmpty();
            RuleFor(p => p.PostalCode).NotEmpty();
            RuleFor(p => p.City).NotEmpty();
        }
    }
}
