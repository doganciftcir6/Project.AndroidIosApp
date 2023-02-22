using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.DeviceTypeDtos;
using Project.AndroidIosApp.Dtos.OSDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IDeviceTypeService
    {
        Task<IDataResponse<CreateDeviceTypeDto>> InsertAsync(CreateDeviceTypeDto createDeviceTypeDto);
        Task<IDataResponse<UpdateDeviceTypeDto>> UpdateAsync(UpdateDeviceTypeDto updateDeviceTypeDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetDeviceTypeDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
