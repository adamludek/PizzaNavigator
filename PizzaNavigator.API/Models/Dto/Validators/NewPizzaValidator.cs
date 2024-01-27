using FluentValidation;

namespace PizzaNavigator.API.Models.Dto.Validators
{
    public class NewPizzaValidator : AbstractValidator<NewPizzaaDto>
    {
        public NewPizzaValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.PriceSmall).GreaterThan(0);
            RuleFor(p => p.PriceMedium).GreaterThan(0);
            RuleFor(p => p.PriceLarge).GreaterThan(0);
        }
    }
}
