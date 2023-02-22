using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.SocialMediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface ISocialMediaService
    {
        Task<IDataResponse<CreateSocialMediaDto>> InsertAsync(CreateSocialMediaDto createSocialMediaDto);
        Task<IDataResponse<UpdateSocialMediaDto>> UpdateAsync(UpdateSocialMediaDto updateSocialMediaDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetSocialMediaDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
