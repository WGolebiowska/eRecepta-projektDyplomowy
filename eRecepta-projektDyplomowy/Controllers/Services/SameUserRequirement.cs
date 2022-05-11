using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using eRecepta_projektDyplomowy.Data;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    #region snippet_HandlerAndRequirement
    public class AppointmentAuthorizationHandler :
        AuthorizationHandler<SameUserRequirement, string[]>
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentAuthorizationHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameUserRequirement requirement,
                                                       string[] ids)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            Claim identityClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == identityClaim.Value);

            if (context.User.HasClaim(c => c.Value == "doctor"))
            {
                if(user.Id == ids[1])
                {
                    context.Succeed(requirement);
                }
            }
            if (context.User.HasClaim(c => c.Value == "patient"))
            {
                if (user.Id == ids[0])
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

    public class SameUserRequirement : IAuthorizationRequirement { }
    #endregion
}