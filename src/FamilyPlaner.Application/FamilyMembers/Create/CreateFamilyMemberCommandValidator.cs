using FamilyPlaner.Application.Common.Interfaces;
using FluentValidation;

namespace FamilyPlaner.Application.FamilyMembers.Create;

public class CreateFamilyMemberCommandValidator : AbstractValidator<CreateFamilyMemberCommand>
{
    public CreateFamilyMemberCommandValidator(IIdentityService identityService)
    {
        RuleFor(v => v.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(64).WithMessage("Username must not exceed 64 characters.")
            .MustAsync(identityService.IsUserNameUnique).WithMessage("The specified username already exists.");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(v => v.DayOfBirth)
            .NotEmpty().WithMessage("Day of birth is required.")
            .GreaterThan(DateOnly.FromDateTime(DateTime.UnixEpoch.AddYears(-70))).WithMessage("Day of birth must be after 01.01.1900.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Day of birth cant be in future.");

        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(128).WithMessage("Email must not exceed 128 characters.")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("The specified email address seems not to be a valid email address.")
            .MustAsync(identityService.IsEmailUnique).WithMessage("The specified email address already exists.");
    }
}
