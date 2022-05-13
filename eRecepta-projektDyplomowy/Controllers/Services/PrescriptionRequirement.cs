using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using eRecepta_projektDyplomowy.Data;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    #region snippet_HandlerAndRequirement
    public class PrescriptionAuthorizationHandler :
       AuthorizationHandler<PrescriptionRequirement, string>
    {
        private readonly ApplicationDbContext _dbContext;

        public PrescriptionAuthorizationHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       PrescriptionRequirement requirement,
                                                       string patientId)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            Claim identityClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == identityClaim.Value);

            if (context.User.HasClaim(c => c.Value == "doctor"))
            {
                context.Succeed(requirement);
            }
            if (context.User.HasClaim(c => c.Value == "patient"))
            {
                if (user.Id == patientId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

    public class PrescriptionRequirement : IAuthorizationRequirement { }
    #endregion
}