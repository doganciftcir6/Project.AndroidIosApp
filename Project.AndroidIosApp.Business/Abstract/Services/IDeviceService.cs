using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IDeviceService
    {
        Task<IDataResponse<CreateDeviceDto>> InsertAsync(CreateDeviceDto createDeviceDto);
        Task<IDataResponse<UpdateDeviceDto>> UpdateAsync(UpdateDeviceDto updateDeviceDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IDataResponse<GetDeviceDto>> GetByIdWithOSDeviceTypeAsync(int id);
        Task<IDataResponse<List<GetDeviceDto>>> GetAllAsync();
        Task<IDataResponse<List<GetDeviceDto>>> GetAllBySortingToCreateDateWithOsDeviceTypeAsync();
        Task<IDataResponse<List<GetDeviceDto>>> GetAllBySortingToTotalScoreWithOsDeviceTypeAsync();
        Task<IDataResponse<List<GetDeviceDto>>> GetAllWithOSAndDeviceTypeAsync();
    }
}
