using Blogging_Platform.DTOs.UserDTOs;
using FluentValidation;

namespace Blogging_Platform.Validators;
public class RegistrationForLoginValidator : AbstractValidator<UserLoginDto>
{
    public RegistrationForLoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 character");
    }
}
