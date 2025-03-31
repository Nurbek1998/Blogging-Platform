using Blogging_Platform.DTOs.CommentDTOs;
using FluentValidation;

namespace Blogging_Platform.Validators;
public class CommentModelValidator : AbstractValidator<CommentForUpdateDto>
{
    public CommentModelValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content cannot be empty or null");
    }
}
