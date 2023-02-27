using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.ProjectRole;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IProjectUserService
    {
        Task<IResponse> InsertWithRoleAsync(CreateProjectUserDto createProjectUserDto, int roleId);
        Task<IDataResponse<UpdateProjectUserDto>> UpdateAsync(UpdateProjectUserDto updateProjectUserDto);
        Task<IDataResponse<GetProjectUserDto>> CheckUserAsync(LoginProjectUserDto loginProjectUserDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetProjectUserDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IDataResponse<List<GetProjectRoleDto>>> GetRolesByUserIdAsync(int userId);
        Task<IDataResponse<GetProjectUserDto>> FindByUserNameAsync(string userName);
        Task<IDataResponse<GetProjectUserDto>> FindByEmailAsync(string email);
    }
}
