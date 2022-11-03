using FluentValidation;

namespace FamilyPlaner.Application.FamilyMembers.Authenticate;

public class AuthenticateFamilyMemberCommandValidator : AbstractValidator<AuthenticateFamilyMemberCommand>
{
    public AuthenticateFamilyMemberCommandValidator()
    {
        RuleFor(v => v.UserName)
           .NotEmpty().WithMessage("Username is required.");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
