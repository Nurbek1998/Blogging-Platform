using Blogging_Platform.DTOs.PostDTOs;
using FluentValidation;

namespace Blogging_Platform.Validators
{
    public class PostForCreationDtoValidator : AbstractValidator<PostModel>
    {
        public PostForCreationDtoValidator()
        {
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Title is required and cannot be empty or null")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 character");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");

            RuleFor(x => x.Status)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Status is required")
                .MaximumLength(20)
                .Must(x => x == "Draft" || x == "Published");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required");

            //RuleFor(x => x.UserId)
            //    .NotEmpty().WithMessage("UserId is required");

        }
    }
}
