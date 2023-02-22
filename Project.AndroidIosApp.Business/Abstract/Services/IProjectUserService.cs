using Project.AndroidIosApp.Core.Utilities.Results.Interface;
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
        Task<IDataResponse<CreateProjectUserDto>> InsertAsync(CreateProjectUserDto createProjectUserDto);
        Task<IDataResponse<UpdateProjectUserDto>> UpdateAsync(UpdateProjectUserDto updateProjectUserDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetProjectUserDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
