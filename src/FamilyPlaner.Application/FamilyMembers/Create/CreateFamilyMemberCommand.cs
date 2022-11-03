using AutoMapper;
using FamilyPlaner.Application.Common.Exceptions;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Entities;
using MediatR;

namespace FamilyPlaner.Application.FamilyMembers.Create;

[Authorize(Roles = FamilyMemberRoles.Administrator)]
public record CreateFamilyMemberCommand : IRequest<CreatedFamilyMemberModel>
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; } = "";
    public DateOnly DayOfBirth { get; set; } = DateOnly.MinValue;
}

public class CreateFamilyMemberCommandHandler : IRequestHandler<CreateFamilyMemberCommand, CreatedFamilyMemberModel>
{

    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateFamilyMemberCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<CreatedFamilyMemberModel> Handle(CreateFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        if (request.DayOfBirth == DateOnly.MinValue)
            request.DayOfBirth = DateOnly.FromDateTime(DateTime.Now);

        Result<Guid> result = await _identityService.CreateUserAsync(request.Username, request.Password, request.Email, request.DayOfBirth);

        if (!result.Succeeded)
            throw new OperationFailedException("Error creating user", result.Errors);

        FamilyMember member = await _identityService.GetUserInfoAsync(result.Data);

        return _mapper.Map<CreatedFamilyMemberModel>(member);
    }
}
