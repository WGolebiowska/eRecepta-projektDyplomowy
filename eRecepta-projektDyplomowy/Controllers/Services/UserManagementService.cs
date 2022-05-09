using AutoMapper;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static eRecepta_projektDyplomowy.Services.Helpers.RoleHelpers;

namespace eRecepta_projektDyplomowy.Controllers.Services
{
    public class UserManagementService : BaseService, IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserManagementService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityRole> GetRoleByNameAsync(string name)
        {
            return await DbContext.Roles.AsNoTracking().Where(role => role.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetAllUsersCountAsync(string searchString)
        {
            var users = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
                users = users.Where(user => (user.Surname.Contains(searchString)
                    || user.Name.Contains(searchString)
                    || user.Email.Contains(searchString)
                    || user.PESEL.Contains(searchString)
                    || user.PhoneNumber.Contains(searchString)));

            return await users.CountAsync();
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync(string searchString)
        {
            var users = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
                users = users.Where(user => (user.Surname.Contains(searchString)
                    || user.Name.Contains(searchString)
                    || user.Email.Contains(searchString)
                    || user.PESEL.Contains(searchString)
                    || user.PhoneNumber.Contains(searchString)));

            return await users.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetUsersAsync(int offset, int limit, string sortOrder, string searchString)
        {
            offset = offset < 0 ? 0 : offset;
            limit = limit < 0 ? 0 : limit;

            var pageUsers = _userManager.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(searchString))
                pageUsers = pageUsers.Where(user => (user.Surname.Contains(searchString)
                    || user.Name.Contains(searchString)
                    || user.Email.Contains(searchString)
                    || user.PESEL.Contains(searchString)
                    || user.PhoneNumber.Contains(searchString)));

            pageUsers = sortOrder switch
            {
                "Lname" => pageUsers.OrderBy(u => u.Surname),
                "Lname_desc" => pageUsers.OrderByDescending(u => u.Surname),
                "Fname" => pageUsers.OrderBy(u => u.Name),
                "Fname_desc" => pageUsers.OrderByDescending(u => u.Name),
                "Email" => pageUsers.OrderBy(u => u.Email),
                "Email_desc" => pageUsers.OrderByDescending(u => u.Email),
                "Approved" => pageUsers.OrderBy(u => u.Approved),
                "Approved_desc" => pageUsers.OrderByDescending(u => u.Approved),
                "PESEL" => pageUsers.OrderBy(u => u.PESEL),
                "PESEL_desc" => pageUsers.OrderByDescending(u => u.PESEL),
                "PhoneNumber" => pageUsers.OrderBy(u => u.PhoneNumber),
                "PhoneNumber_desc" => pageUsers.OrderByDescending(u => u.PhoneNumber),
                "Role" => pageUsers.OrderBy(u => u.UserType),
                "Role_desc" => pageUsers.OrderByDescending(u => u.UserType),
                _ => pageUsers.OrderBy(u => u.Surname),
            };
            pageUsers = pageUsers.Skip(offset).Take(limit);

            return await pageUsers.ToListAsync();
        }
        public async Task<string> GetUserRoleAsync(string userId, bool returnName)
        {
            ApplicationUser user = await DbContext.Users.AsNoTracking().Where(u => u.Id == userId).FirstOrDefaultAsync();
            var roles = await _userManager.GetRolesAsync(user);

            foreach (RolePair rolePair in RoleHelpers.Roles)
            {
                IdentityRole identityRole = await DbContext.Roles.AsNoTracking().Where(role => role.Name == rolePair.Name)
                    .FirstOrDefaultAsync();
                if (identityRole != null && roles.Contains(identityRole.Name))
                    return returnName ? rolePair.Name : rolePair.Description;
            }
            return "";
        }
        public async Task<string> GetUserRoleAsync(string email)
        {
            ApplicationUser user = await DbContext.Users.AsNoTracking().Where(u => u.Email == email).FirstOrDefaultAsync();
            var roles = await _userManager.GetRolesAsync(user);

            foreach (RolePair rolePair in RoleHelpers.Roles)
            {
                IdentityRole identityRole = await DbContext.Roles.AsNoTracking().Where(role => role.Name == rolePair.Name).FirstOrDefaultAsync();
                if (roles.Contains(identityRole.Name))
                    return rolePair.Name;
            }
            return "";
        }
        public async Task<ApplicationUser> FindUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }
        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string password, string role)
        {
            if (await _userManager.FindByEmailAsync(user.Email) != null)
                return IdentityResult.Failed(new IdentityError() { Description = "Ten adres Email istnieje już w systemie!" });
            if (role == "administrator")
            {
                user.UserType = 0;
            }
            else if (role == "doctor")
            {
                user.UserType = 1;
            }
            else
            {
                user.UserType = 2;
            }
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, role);

            return result;
        }
        public async Task<IdentityResult> AddUserAsync(Doctor user, string password, string role)
        {
            if (await _userManager.FindByEmailAsync(user.Email) != null)
                return IdentityResult.Failed(new IdentityError() { Description = "Ten adres Email istnieje już w systemie!" });
            if (role == "administrator")
            {
                user.UserType = 0;
            }
            else if (role == "doctor")
            {
                user.UserType = 1;
            }
            else
            {
                user.UserType = 2;
            }
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, role);

            return result;
        }
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user, string newUserRole)
        {
            if (newUserRole == "administrator")
            {
                user.UserType = 0;
            }
            else if (newUserRole == "doctor")
            {
                user.UserType = 1;
            }
            else
            {
                user.UserType = 2;
            }
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null && existingUser.Id != user.Id)
                return IdentityResult.Failed(new IdentityError() { Description = "Ten adres Email istnieje już w systemie!" });

            DbContext.Entry(user).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();

            string[] existingRoles = (await _userManager.GetRolesAsync(user)).ToArray();
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);

            if (result.Succeeded)
                result = await _userManager.AddToRoleAsync(user, newUserRole);

            return result;
        }
        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            return await _userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
                result = await _userManager.AddPasswordAsync(user, password);

            return result;
        }
        public async Task<bool> IsEmailInUseAsync(string email, string excludeUserID)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            return user != null && user.Id != excludeUserID;
        }
        public async Task<bool> IsEmailInUseAsync(string email)
        {
            return await IsEmailInUseAsync(email, null);
        }
        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await _userManager.GetUserAsync(claimsPrincipal);
        }
    }
}
