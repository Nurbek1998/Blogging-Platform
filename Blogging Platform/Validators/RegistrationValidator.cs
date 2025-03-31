using Blogging_Platform.DTOs.UserDTOs;
using FluentValidation;

namespace Blogging_Platform.Validators;
public class RegistrationValidator : AbstractValidator<UserRegisterDto>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 length");

        RuleFor(x => x.Role)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Username is required")
            .Must(role => role == "admin" || role == "author" || role == "moderator" || role == "reader")
            .WithMessage("Role must be one of the following: Admin, Author, Moderator, Reader.");
    }
}
