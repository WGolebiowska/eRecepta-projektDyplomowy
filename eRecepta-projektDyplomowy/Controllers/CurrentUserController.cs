using AutoMapper;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrentUserController : BaseController
    {
        private readonly ClaimsPrincipal _caller;
        private readonly ApplicationDbContext _dbContext;

        public CurrentUserController(IMapper mapper, ILogger logger, ClaimsPrincipal caller, ApplicationDbContext dbContext) : base(mapper, logger)
        {
            _caller = caller;
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var identity = User.Identity as ClaimsIdentity;
            Claim identityClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var result = _dbContext.Users.FirstOrDefault(u => u.Id == identityClaim.Value);
            return Ok(result);

        }
    }
}