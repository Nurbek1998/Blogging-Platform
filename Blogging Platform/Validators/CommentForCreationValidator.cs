using Blogging_Platform.DTOs.CommentDTOs;
using FluentValidation;

namespace Blogging_Platform.Validators;
public class CommentForCreationValidator : AbstractValidator<CommentForCreationDto>
{
    public CommentForCreationValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content cannot be empty or null");

        RuleFor(x => x.PostId).
            NotEmpty().WithMessage("PostId cannot be empty")
            .Must(x => x != Guid.Empty).WithMessage("PostId cannot be an empty Guid.");
    }
}
