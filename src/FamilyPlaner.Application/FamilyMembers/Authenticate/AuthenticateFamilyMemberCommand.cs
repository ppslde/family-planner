using AutoMapper;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Domain.Entities;
using MediatR;

namespace FamilyPlaner.Application.FamilyMembers.Authenticate;

public record AuthenticateFamilyMemberCommand : IRequest<AuthenticatedUserModel>
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}

public class AuthenticateFamilyMemberCommandHandler : IRequestHandler<AuthenticateFamilyMemberCommand, AuthenticatedUserModel>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public AuthenticateFamilyMemberCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<AuthenticatedUserModel> Handle(AuthenticateFamilyMemberCommand request, CancellationToken cancellationToken)
    {
        Guid userId = await _identityService.AuthenticateUser(request.UserName, request.Password);

        if (userId == Guid.Empty)
            throw new UnauthorizedAccessException();

        FamilyMember user = await _identityService.GetUserInfoAsync(userId);

        return new()
        {
            UserInfo = _mapper.Map<UserInfoModel>(user),
            AccessToken = _identityService.GenerateUserToken(user)
        };
    }
}
