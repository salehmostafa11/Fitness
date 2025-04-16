using FitCore.Dto.Authentication;

namespace Fit.Authorization
{
    public class AdminAuthorizeAttribute : RoleAuthorizeAttribute
    {
        public AdminAuthorizeAttribute() : base(ApplicationRoles.AdminRole)
        {
            
        }
    }
}
