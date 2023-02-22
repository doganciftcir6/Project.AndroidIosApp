using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.SupportUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface ISupportUserService
    {
        Task<IDataResponse<CreateSupportUserDto>> InsertAsync(CreateSupportUserDto createSupportUserDto);
        Task<IDataResponse<UpdateSupportUserDto>> UpdateAsync(UpdateSupportUserDto updateSupportUserDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetSupportUserDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
