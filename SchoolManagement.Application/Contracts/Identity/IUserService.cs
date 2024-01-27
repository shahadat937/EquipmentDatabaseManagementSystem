using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.Models.Identity;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<BaseCommandResponse> UpdateUserPassword(string userId,PasswordChangeDto userDto); 
        Task<Employee> GetEmployeeByUserId(string userId);
        Task<Employee> GetEmployee(string userId);
        Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams);
        Task<BaseCommandResponse> Save(string userId,CreateUserDto user);
        Task<BaseCommandResponse> ResetPassword(string userId,CreateUserDto user);
        Task<UserDto> GetUserById(string id);
        Task<BaseCommandResponse> DeleteUser(string id);
        //Task<PagedResult<UserDto>> GetStudentUsers(QueryParams queryParams);
        Task<PagedResult<UserDto>> GetTeacherUsers(QueryParams queryParams);
    }
}
