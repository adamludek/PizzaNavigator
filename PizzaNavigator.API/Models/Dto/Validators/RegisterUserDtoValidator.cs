using FluentValidation;

namespace PizzaNavigator.API.Models.Dto.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(p =>  p.Email).NotEmpty().EmailAddress();
            RuleFor(p =>  p.Password).NotEmpty();
            RuleFor(p =>  p.FirstName).NotEmpty();
            RuleFor(p =>  p.LastName).NotEmpty();
            RuleFor(p =>  p.Username).NotEmpty();
        }
    }
}
