using FitCore.Dto.Authentication;

namespace Fit.Authorization
{ 
    public class TraineeAuthorizeAttribute : RoleAuthorizeAttribute
    {
        public TraineeAuthorizeAttribute() : base(ApplicationRoles.TraineesRole)
        {
        }
    }

}
