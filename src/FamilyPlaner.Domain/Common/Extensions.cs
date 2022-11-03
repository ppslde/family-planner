using FamilyPlaner.Domain.Entities;

namespace FamilyPlaner.Domain.Common;

public static class Extensions
{
    public static bool IsSameUser(this FamilyMember familyMember, FamilyMember other)
    {
        return familyMember.Name.ToLower() == other.Name.ToLower() &&
                familyMember.Email.ToLower().ToLower() == other.Email.ToLower();
    }

    public static T[] GetFlags<T>(this T roles) where T : Enum
    {
        List<T> flags = new();
        foreach (T flag in Enum.GetValues(typeof(T)))
        {
            if (roles.HasFlag(flag))
                flags.Add(flag);
        }
        return flags.ToArray();
    }
}
