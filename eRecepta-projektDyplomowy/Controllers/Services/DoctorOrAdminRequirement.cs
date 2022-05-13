using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using System.Diagnostics;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    #region snippet_HandlerAndRequirement
    public class DoctorOrAdminAuthorizationHandler :
        AuthorizationHandler<DoctorOrAdminRequirement>
    {

        public DoctorOrAdminAuthorizationHandler(){}

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       DoctorOrAdminRequirement requirement)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            Claim identityClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (context.User.HasClaim(c => c.Value == "doctor") || context.User.HasClaim(c => c.Value == "administrator"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class DoctorOrAdminRequirement: IAuthorizationRequirement { }
    #endregion
}