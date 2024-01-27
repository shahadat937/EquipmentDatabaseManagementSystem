using AutoMapper;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Models.Identity;
using SchoolManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Common.Validators;
using System.ComponentModel.DataAnnotations;
using SchoolManagement.Application.Responses;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.DTOs.Role;
using SchoolManagement.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleService(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SelectedModel>> GetSelectedRoleList()
        {
            ICollection<IdentityRole> roles =await _roleManager.Roles.ToListAsync();
            string[] role = {CustomRoleTypes.Student,CustomRoleTypes.Instructor};
            List<SelectedModel> selectModels = roles.Where(x => !role.Contains(x.Name)).Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.Name
            }).ToList();
            return selectModels;
        }

        public async Task<List<SelectedModel>> GetSelectedAllRoleList()
        {
            ICollection<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            string[] role = { CustomRoleTypes.Student, CustomRoleTypes.Instructor };
            List<SelectedModel> selectModels = roles.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.Name
            }).ToList();
            return selectModels;
        }

        public async Task<List<SelectedModel>> GetSelectedRoleForTraineeList()
        {
            ICollection<IdentityRole> roles =await _roleManager.Roles.ToListAsync();
            string[] role = {CustomRoleTypes.Student,CustomRoleTypes.Instructor};
            List<SelectedModel> selectModels = roles.Where(x => role.Contains(x.Name)).Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.Name
            }).ToList();
            return selectModels;
        }

        public async Task<BaseCommandResponse> Save(string roleId, CreateRoleDto model)
        {
            var response = new BaseCommandResponse();

            if (!String.IsNullOrWhiteSpace(roleId))
            {
                var role = _roleManager.Roles.SingleOrDefault(x => x.Id == roleId);

                if (role == null)
                {
                    throw new BadRequestException("Role not found !");
                }
               
                role.Name = model.RoleName;
                await _roleManager.UpdateAsync(role);
                response.Success = true;
                response.Message = "Updated Successful";
                // response.Id = User.Id;
            }

            else
            {
                var Role = new IdentityRole()
                {
                    Name = model.RoleName,
                    NormalizedName = model.RoleName.ToUpper()
                };
                await _roleManager.CreateAsync(Role);
                response.Success = true;
                response.Message = "Creation Successful";
                // response.Id = User.Id;
            }

            return response;
        }


        //public async Task<Employee> GetEmployee(string userId)
        //{
        //    var employee = await _userManager.FindByIdAsync(userId);
        //    return new Employee
        //    {
        //        Email = employee.Email,
        //        Id = employee.Id,
        //        Firstname = employee.FirstName,
        //        Lastname = employee.LastName
        //    };
        //}

        //public async Task<List<Employee>> GetEmployees()
        //{
        //    var employees = await _userManager.GetUsersInRoleAsync("Employee");
        //    return employees.Select(q => new Employee { 
        //        Id = q.Id,
        //        Email = q.Email,
        //        Firstname = q.FirstName,
        //        Lastname = q.LastName
        //    }).ToList();
        //}

        //public async Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams)
        //{
        //    var validator = new QueryParamsValidator();
        //    var validationResult = await validator.ValidateAsync(queryParams);

        //    if (validationResult.IsValid == false)
        //        throw new FluentValidation.ValidationException(validationResult.ToString());

        //    IQueryable<ApplicationUser> userQuery = _userManager.Users.AsQueryable();
        //    var totalCount = userQuery.Count();
        //    userQuery = userQuery.OrderByDescending(x => x.UserName).Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);

        //    var UsersDtos = userQuery.Select(q => new UserDto
        //    {
        //        Id = q.Id,
        //        Email = q.Email,
        //        FirstName = q.FirstName,
        //        LastName = q.LastName
        //    }).ToList();

        //    var result = new PagedResult<UserDto>(UsersDtos, totalCount, queryParams.PageNumber, queryParams.PageSize);

        //    return result;

        //}

        //public async Task<BaseCommandResponse> Save(CreateUserDto userDto)
        //{
        //    var response = new BaseCommandResponse();




        //    var existingUser = await _userManager.FindByNameAsync(userDto.UserName);

        //    if (existingUser != null)
        //    {
        //        throw new BadRequestException($"Username '{userDto.UserName}' already exists.");
        //    }

        //    var User = new ApplicationUser
        //    {
        //        Email = userDto.Email,
        //        FirstName = userDto.FirstName,
        //        LastName = userDto.LastName,
        //        UserName = userDto.UserName,
        //        RoleName = userDto.RoleName,
        //        BranchId = userDto.BranchId,
        //        CreatedBy = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value,
        //        CreatedDate = DateTime.Now,
        //        InActiveBy = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value,
        //        InActiveDate=DateTime.Now,
        //        IsActive=true,
        //        EmailConfirmed = true
        //    };

        //    var existingEmail = await _userManager.FindByEmailAsync(userDto.Email);

        //    if (existingEmail == null)
        //    {

        //        var result = await _userManager.CreateAsync(User, userDto.Password);

        //        if (result.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(User, userDto.RoleName);

        //            response.Success = true;
        //            response.Message = "Creation Successful";
        //           // response.Id = User.Id;
        //        }
        //        else
        //        {
        //            throw new BadRequestException($"{result.Errors}");
        //        }
        //    }
        //    else
        //    {
        //        throw new BadRequestException($"Email {userDto.Email } already exists.");
        //    }


        //    return response;
        //}


    }
}
