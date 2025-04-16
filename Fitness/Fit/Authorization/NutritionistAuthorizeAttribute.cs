using FitCore.Dto.Authentication;

namespace Fit.Authorization
{
    public class NutritionistAuthorizeAttribute : RoleAuthorizeAttribute
    {
        public NutritionistAuthorizeAttribute() : base(ApplicationRoles.NutritionistsRole)
        {

        }
    }
}
