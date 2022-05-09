﻿using eRecepta_projektDyplomowy.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Controllers.Services.Interfaces
{
    public interface IUserManagementService
    {
        Task<IdentityRole> GetRoleByNameAsync(string name);
        Task<int> GetAllUsersCountAsync(string searchString);
        Task<List<ApplicationUser>> GetAllUsersAsync(string searchString);
        Task<List<ApplicationUser>> GetUsersAsync(int offset, int limit, string sortOrder, string searchString);
        Task<string> GetUserRoleAsync(string userId, bool returnName);
        Task<string> GetUserRoleAsync(string email);
        Task<ApplicationUser> FindUserAsync(string userId);
        Task<IdentityResult> AddUserAsync(ApplicationUser user, string password, string role);
        Task<IdentityResult> AddUserAsync(Doctor user, string password, string role);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user, string newUserRole);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string password);
        Task<bool> IsEmailInUseAsync(string email, string excludeUserID);
        Task<bool> IsEmailInUseAsync(string email);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal);
    }
}