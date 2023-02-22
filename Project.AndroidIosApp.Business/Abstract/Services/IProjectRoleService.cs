using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.ProjectRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IProjectRoleService
    {
        Task<IDataResponse<CreateProjectRoleDto>> InsertAsync(CreateProjectRoleDto createProjectRoleDto);
        Task<IDataResponse<UpdateProjectRoleDto>> UpdateAsync(UpdateProjectRoleDto updateProjectRoleDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetProjectRoleDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
