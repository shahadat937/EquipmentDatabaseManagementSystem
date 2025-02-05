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
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Identity.Services
{
    public class UserService : IUserService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;
        //private readonly ISchoolManagementRepository<TraineeBioDataGeneralInfo> _BiodataRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, ISchoolManagementRepository<BaseSchoolName> BaseSchoolNameRepository, IHttpContextAccessor httpContextAccessor)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _BaseSchoolNameRepository = BaseSchoolNameRepository;
            //_BiodataRepository = BiodataRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }
        //public async Task<BaseCommandResponse> UpdateUserPassword(string userId,PasswordChangeDto userDto)
        //{
        //    var response = new BaseCommandResponse();

        //    var user = await _userManager.FindByIdAsync(userId);
        //    await _userManager.ChangePasswordAsync(user, userDto.CurrentPassword, userDto.NewPassword);
        //    await _signinManager.RefreshSignInAsync(user);

        //    return response;
        //}

        public async Task<BaseCommandResponse> UpdateUserPassword(string userId, PasswordChangeDto userDto)
        {
            var response = new BaseCommandResponse();

            // Fetch the user from the database
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                return response;
            }

            // Check if the current password is correct
            var passwordValid = await _userManager.CheckPasswordAsync(user, userDto.CurrentPassword);
            if (!passwordValid)
            {
                throw new BadRequestException("Current Password Not Matched !!");
            }

            // Proceed with changing the password
            var result = await _userManager.ChangePasswordAsync(user, userDto.CurrentPassword, userDto.NewPassword);

            if (result.Succeeded)
            {
                // Optionally, refresh the user's sign-in session
                await _signinManager.RefreshSignInAsync(user);

                response.Success = true;
                response.Message = "Password updated successfully.";
            }
            else
            {
                // Handle failure to change password
                response.Success = false;
                response.Message = "Failed to change password.";
            }

            return response;
        }


        public async Task<BaseCommandResponse> ResetPassword(string userId, CreateUserDto userDto)
        {
            var response = new BaseCommandResponse();

            var user = await _userManager.FindByIdAsync(userId);
          
           
            if (user == null)
                throw new BadRequestException("Invalide Request !!");
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            string newPassword = "Admin@123";
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
           
            if (!resetPassResult.Succeeded)
            {
                throw new BadRequestException(resetPassResult.Errors.ToString());
               
            }
            //await _userManager.ChangePasswordAsync(user, user.p, userDto.NewPassword);
            //var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            ////await _userManager.ResetPasswordAsync(user, resetToken, userDto.Password);
            //await _signinManager.RefreshSignInAsync(user);
            // debug doren
            return response;
        }
        //public async Task<Employee> UpdateUserPassword(string userId, PasswordChangeDto userDto)
        //{
        //    var User = new ApplicationUser();

        //    if (!string.IsNullOrWhiteSpace(userId))
        //    {
        //        User = _userManager.Users.FirstOrDefault(x => x.Id == userId);
        //        User.PasswordHash = userDto.Password;
        //        await _userManager.UpdateAsync(User);
        //    }

        //    var employee = await _userManager.FindByIdAsync(userId);
        //    return new Employee
        //    {
        //        Email = employee.Email,
        //        Id = employee.Id,
        //        Firstname = employee.FirstName,
        //        Lastname = employee.LastName,
        //        Password = userDto.Password,
        //        ConfirmPassword = userDto.ConfirmPassword
        //    };
        //}

        public async Task<Employee> GetEmployeeByUserId(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                Firstname = employee.FirstName,
                Lastname = employee.LastName
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(q => new Employee { 
                Id = q.Id,
                Email = q.Email,
                Firstname = q.FirstName,
                Lastname = q.LastName
            }).ToList();
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userDto = new UserDto();
                
            userDto = new UserDto
            {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    PhoneNumber = user.PhoneNumber,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    RoleName = user.RoleName
            };

            if (user.BranchId != null && !String.IsNullOrEmpty(user.BranchId))
            {
                BaseSchoolName baseSchoolName = await _BaseSchoolNameRepository.Where(x => x.BaseSchoolNameId == Convert.ToInt16(user.BranchId)).FirstOrDefaultAsync();

                userDto.FirstLevel = baseSchoolName.FirstLevel;
                userDto.SecondLevel = baseSchoolName.SecondLevel;
                userDto.ThirdLevel = baseSchoolName.ThirdLevel;
                userDto.FourthLevel = baseSchoolName.FourthLevel;
            }

            return userDto;

        }

        //public async Task<BaseCommandResponse> DeleteUser(string id)
        //{
        //    var response = new BaseCommandResponse();


        //    var user = await _userManager.FindByIdAsync(id);
        //    await _userManager.DeleteAsync(user);
        //    response.Success = true;
        //    response.Message = "Deleted Successful";
        //    return response;


        //}

        public async Task<BaseCommandResponse> DeleteUser(string userId)
        {
            var response = new BaseCommandResponse();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new BadRequestException("Invalide Request !!");
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new BadRequestException("User not found.");
            }
            user.IsActive = false;
            if (DeleteUserRoles(user))
            {
                IdentityResult result = _userManager.DeleteAsync(user).Result;
                response.Success = result.Succeeded;
                response.Message = "Deleted Successful";
                return response;
            }
            response.Success = false;
            response.Message = "Deleted Failed";
            return response;
        }

        public bool DeleteUserRoles(ApplicationUser user)
        {
           
            IList<string> userRoles = _userManager.GetRolesAsync(user).Result;
            if (userRoles.Any())
            {
                return  _userManager.RemoveFromRolesAsync(user, userRoles).Result.Succeeded;
            }

            return true;
        }

        public async Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(queryParams);

            if (validationResult.IsValid == false)
                throw new FluentValidation.ValidationException(validationResult.ToString());

            IQueryable<ApplicationUser> userQuery = _userManager.Users.AsQueryable();
            string[] searchingRoles = { CustomRoleTypes.Student, CustomRoleTypes.Instructor };
            var userQueryRoleFilter = userQuery.Where(x => !searchingRoles.Contains(x.RoleName));
            userQuery = userQueryRoleFilter.Where(x => (x.UserName.Contains(queryParams.SearchText)) || String.IsNullOrEmpty(queryParams.SearchText));
            var totalCount = userQuery.Count();
            userQuery = userQuery.OrderByDescending(x => x.UserName).Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);

            var UsersDtos = userQuery.Select(q => new UserDto
            {
                Id = q.Id,
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName,
                UserName = q.UserName,
                PhoneNumber =q.PhoneNumber,
                RoleName = q.RoleName

            }).ToList();

            var result = new PagedResult<UserDto>(UsersDtos, totalCount, queryParams.PageNumber, queryParams.PageSize);

            return result;

        }

        public async Task<BaseCommandResponse> Save(string userId,CreateUserDto userDto)
        {
            var response = new BaseCommandResponse();

        
                var User = new ApplicationUser();
                bool isNotSameRole = false;
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    User = _userManager.Users.FirstOrDefault(x => x.Id == userId);
                isNotSameRole = User.RoleName != userDto.RoleName;
                }
                
                User.Email = userDto.Email;
                User.FirstName = userDto.FirstName;
                User.PhoneNumber = userDto.PhoneNumber;
                User.LastName = userDto.LastName;
                User.UserName = userDto.UserName;
                User.RoleName = userDto.RoleName;
                User.BranchId = !String.IsNullOrWhiteSpace(userDto.FourthLevel) ? userDto.FourthLevel : !String.IsNullOrWhiteSpace(userDto.ThirdLevel) ? userDto.ThirdLevel : !String.IsNullOrWhiteSpace(userDto.SecondLevel) ? userDto.SecondLevel : userDto.FirstLevel;
                User.PNo = userDto.TraineeId;
                User.CreatedBy = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
                User.CreatedDate = DateTime.Now;
                User.InActiveBy = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
                User.InActiveDate = DateTime.Now;
                User.IsActive = true;
                User.EmailConfirmed = true;

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    await _userManager.UpdateAsync(User);
                if (isNotSameRole)
                {
                    IList<string> userRoles =await _userManager.GetRolesAsync(User);
                    if (userRoles.Any())
                    {
                      await _userManager.RemoveFromRolesAsync(User, userRoles);
                    }
                  //  await _userManager.RemoveFromRoleAsync(User, userDto.RoleName);
                    await _userManager.AddToRoleAsync(User, userDto.RoleName);
                }
                    response.Success = true;
                    response.Message = "Updated Successful";
                    return response;
                }
                var existingUser = await _userManager.FindByNameAsync(userDto.UserName);

                if (existingUser != null)
                {
                    throw new BadRequestException($"Username '{userDto.UserName}' already exists.");
                }

                var existingEmail = await _userManager.FindByEmailAsync(userDto.Email);

                if (existingEmail == null)
                {
                    var result = await _userManager.CreateAsync(User, userDto.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(User, userDto.RoleName);

                        response.Success = true;
                        response.Message = "Creation Successful";
                       // response.Id = User.Id;
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException($"{result.Errors}");
                        //throw new BadRequestException($"{result.Errors}");
                    }
                }
                else
                {
                    throw new BadRequestException($"Email {userDto.Email } already exists.");
                }

            
            return response;
        }


        public async Task<PagedResult<UserDto>> GetTeacherUsers(QueryParams queryParams)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(queryParams);

            if (validationResult.IsValid == false)
                throw new FluentValidation.ValidationException(validationResult.ToString());

            IQueryable<ApplicationUser> userQuery = _userManager.Users.AsQueryable();

            string[] searchingRoles = { CustomRoleTypes.Instructor, CustomRoleTypes.Student };
            var userQueryRoleFilter = userQuery.Where(x => searchingRoles.Contains(x.RoleName));
            userQuery = userQueryRoleFilter.Where(x => searchingRoles.Contains(x.RoleName) && (x.UserName.Contains(queryParams.SearchText)) || String.IsNullOrEmpty(queryParams.SearchText));
            //userQuery = userQuery.Where(x => (x.UserName.Contains(queryParams.SearchText)) || String.IsNullOrEmpty(queryParams.SearchText));
            var totalCount = userQuery.Count();
            userQuery = userQuery.OrderByDescending(x => x.UserName).Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize);

            var UsersDtos = userQuery.Select(q => new UserDto
            {
                Id = q.Id,
                Email = q.Email,
                FirstName = q.FirstName,
                LastName = q.LastName,
                UserName = q.UserName,
                PhoneNumber = q.PhoneNumber,
                RoleName = q.RoleName
            }).ToList();

            var result = new PagedResult<UserDto>(UsersDtos, totalCount, queryParams.PageNumber, queryParams.PageSize);

            return result;
        }

        //public async Task<List<Employee>> IUserService.GetEmployeeByUserId(string userId)
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

        //public Task<List<Employee>> GetEmployeeByUserId(string userId)
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
    }
}
