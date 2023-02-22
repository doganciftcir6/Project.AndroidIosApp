using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.SupportUserSupportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface ISupportUserSupportService
    {
        Task<IDataResponse<CreateSupportUserSupportDto>> InsertAsync(CreateSupportUserSupportDto createSupportUserSupportDto);
        Task<IDataResponse<UpdateSupportUserSupportDto>> UpdateAsync(UpdateSupportUserSupportDto updateSupportUserSupportDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetSupportUserSupportDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
