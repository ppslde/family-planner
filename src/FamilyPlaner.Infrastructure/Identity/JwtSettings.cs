namespace FamilyPlaner.Infrastructure.Identity;

class JwtSettings
{
    public string? Secret { get; set; }
    public string? Issuer { get; set; }
    public string? Expiration { get; set; }
    public string? Audience { get; set; }
}