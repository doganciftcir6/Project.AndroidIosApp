using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.SupportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface ISupportService
    {
        Task<IDataResponse<CreateSupportDto>> InsertAsync(CreateSupportDto createSupportDto);
        Task<IDataResponse<UpdateSupportDto>> UpdateAsync(UpdateSupportDto updateSupportDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetSupportDto>>> GetAllAsync();
        Task<IDataResponse<List<GetSupportDto>>> GetAllByEmailReceiverAsync(string email);
        Task<IDataResponse<List<GetSupportDto>>> GetAllByEmailSenderAsync(string email);
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IDataResponse<GetSupportDto>> GetByIdWithUserAsync(int id);
    }
}
