using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Role;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Contracts.Identity
{
    public interface IRoleService
    {
        //Task<List<Employee>> GetEmployees();
        //Task<Employee> GetEmployee(string userId);
        //Task<PagedResult<UserDto>> GetUsers(QueryParams queryParams);
        //Task<BaseCommandResponse> Save(CreateUserDto user);
        Task<BaseCommandResponse> Save(string roleId, CreateRoleDto model);
        Task<List<SelectedModel>> GetSelectedRoleList();
        Task<List<SelectedModel>> GetSelectedAllRoleList();
        Task<PagedResult<AspNetRoles>> GetRoles(QueryParams queryParams);
        Task<List<SelectedModel>> GetSelectedRoleForTraineeList();
        Task<BaseCommandResponse> Delete(string id);
        Task<object> GetRoleById(string id);
    }
}
