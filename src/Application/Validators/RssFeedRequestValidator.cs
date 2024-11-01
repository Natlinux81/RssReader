using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class RssFeedRequestValidator : AbstractValidator<RssFeedRequest>
{
    public RssFeedRequestValidator()
    {
        RuleFor( x => x.Url )
            .NotEmpty().WithMessage("Url is required")
            .Equal("https?://.*rss.*").WithMessage("Url is not valid");
    }
}