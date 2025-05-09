﻿using Blogging_Platform.DTOs.TagDTOs;
using FluentValidation;

namespace Blogging_Platform.Validators;
public class TagForCreationDtoValidator : AbstractValidator<TagModel>
{
    public TagForCreationDtoValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Tag name is required")
            .MaximumLength(100).WithMessage("Maximum length for tag is 100 character");
    }
}
