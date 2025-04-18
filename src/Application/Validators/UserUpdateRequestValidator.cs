using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage(user => $"Username must not exceed 50 characters.");
        
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0).WithMessage("Id is required.");
    }
}