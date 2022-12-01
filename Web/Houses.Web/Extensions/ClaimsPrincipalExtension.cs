using System.Security.Claims;
using static Houses.Common.GlobalConstants.ValidationConstants.AdminConstants;

namespace Houses.Web.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdministrator(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.IsInRole(AdministratorRoleName);
    }
}
