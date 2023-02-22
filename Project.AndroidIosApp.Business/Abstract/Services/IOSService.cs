using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.GenderDto;
using Project.AndroidIosApp.Dtos.OSDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IOSService
    {
        Task<IDataResponse<CreateOSDto>> InsertAsync(CreateOSDto createOSDto);
        Task<IDataResponse<UpdateOSDto>> UpdateAsync(UpdateOSDto updateOSDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetOSDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
