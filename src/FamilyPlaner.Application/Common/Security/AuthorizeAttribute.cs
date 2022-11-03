using FamilyPlaner.Domain.Authorization;

namespace FamilyPlaner.Application.Common.Security;

/// <summary>
/// Specifies the class this attribute is applied to requires authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
    /// </summary>
    public AuthorizeAttribute() { }

    /// <summary>
    /// Gets or sets a comma delimited list of roles that are allowed to access the resource.
    /// </summary>
    public FamilyMemberRoles Roles { get; set; } = FamilyMemberRoles.None;

    /// <summary>
    /// Gets or sets the policy name that determines access to the resource.
    /// </summary>
    public FamilyMemberPolicies Policy { get; set; } = FamilyMemberPolicies.None;
}