using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly ClaimsPrincipal _caller;
        private readonly IUserManagementService _umService;
        private readonly ApplicationDbContext _dbContext;

        public CurrentUserController(IMapper mapper, ILogger logger, ClaimsPrincipal caller, ApplicationDbContext dbContext, IUserManagementService umService) : base(mapper, logger)
        {
            _mapper = mapper;
            _caller = caller;
            _dbContext = dbContext;
            _umService = umService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var identity = User.Identity as ClaimsIdentity;
            Claim identityClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == identityClaim.Value);
            var userModel = Mapper.Map<UserModel>(user);
            string userRole = await _umService.GetUserRoleAsync(user.Id, false);
            userModel.Role = userRole;

            return Ok(userModel);

        }
    }
}