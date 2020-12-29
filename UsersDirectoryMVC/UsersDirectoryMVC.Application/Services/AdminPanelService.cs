﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersDirectoryMVC.Application.Interfaces;
using UsersDirectoryMVC.Application.ViewModels.AdminPanel;

namespace UsersDirectoryMVC.Application.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public AdminPanelService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,  IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> ChangeUserRolesAsync(string idUser, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(idUser);
            if (user == null)
            {
                return null;
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            if (roles.ToList().Count > userRoles.Count)
            {
                return await AddRolesToUserAsync(user, roles);
            }
            else
            {
                return await RemoveRolesFromUserAsync(user, roles);
            }
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return await Task.FromResult<IdentityResult>(null);
            }
            return await _userManager.DeleteAsync(user);
        }

        public IQueryable<RoleVm> GetAllRoles()
        {
            var rolesVm = _roleManager.Roles?.ProjectTo<RoleVm>(_mapper.ConfigurationProvider);
            return rolesVm;
        }

        public ListUsersForListVm GetAllUsers()
        {
            var users = _userManager.Users.ProjectTo<UserForListVm>(_mapper.ConfigurationProvider).ToList();
            var usersVm = new ListUsersForListVm()
            {
                Users = users,
                Count = users.Count
            };
            return usersVm;
        }

        public IQueryable<string> GetRolesByUser(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var roles = _userManager.GetRolesAsync(user).Result.AsQueryable();
            return roles;
        }

        public UserDetailVm GetUserDetails(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var userVm = _mapper.Map<UserDetailVm>(user);
            userVm.UserRoles = GetRolesByUser(user.Id).ToList();
            return userVm;
        }

        public UserDetailVm GetUserRoles(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var userVm = _mapper.Map<UserDetailVm>(user);
            userVm.UserRoles = GetRolesByUser(user.Id).ToList();
            userVm.Roles = GetAllRoles().ToList();
            return userVm;
        }

        public void RemoveRoleFromUser(string id, string roles)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return;
            }
            _userManager.RemoveFromRoleAsync(user, roles);
        }

        private async Task<IdentityResult> RemoveRolesFromUserAsync(IdentityUser user, IEnumerable<string> roles)
        {
            var actuallUserRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, actuallUserRoles);
            return await AddRolesToUserAsync(user, roles);

        }

        private async Task<IdentityResult> AddRolesToUserAsync(IdentityUser user, IEnumerable<string> roles)
        {
            IdentityResult result;
            roles = RemoveDuplicateRoles(user, roles);
            result = await _userManager.AddToRolesAsync(user, roles);
            return result;
        }

        private List<string> RemoveDuplicateRoles(IdentityUser user, IEnumerable<string> roles)
        {
            var userRoles = _userManager.GetRolesAsync(user).Result.ToList();
            var rolesToAdd = roles.Where(r => !userRoles.Contains(r)).ToList();
            return rolesToAdd;
        }
    }
}
