using FitCore.Dto.Authentication;

namespace Fit.Authorization
{
    public class TrainerAuthorizeAttribute : RoleAuthorizeAttribute
    {
        public TrainerAuthorizeAttribute():base ( ApplicationRoles.TrainersRole)
        {
            
        }
    }
}
