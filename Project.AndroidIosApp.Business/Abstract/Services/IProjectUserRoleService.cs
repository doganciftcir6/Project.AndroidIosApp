using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.ProjectUserRoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IProjectUserRoleService
    {
        Task<IDataResponse<CreateProjectUserRoleDto>> InsertAsync(CreateProjectUserRoleDto createProjectUserRoleDto);
        Task<IDataResponse<UpdateProjectUserRoleDto>> UpdateAsync(UpdateProjectUserRoleDto updateProjectUserRoleDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetProjectUserRoleDto>>> GetAllAsync();
        Task<IDataResponse<List<GetProjectUserRoleDto>>> GetAllWithProjectUserAndRoleAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
