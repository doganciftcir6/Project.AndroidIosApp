using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.GenderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IGenderService
    {
        Task<IDataResponse<CreateGenderDto>> InsertAsync(CreateGenderDto createGenderDto);
        Task<IDataResponse<UpdateGenderDto>> UpdateAsync(UpdateGenderDto updateGenderDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetGenderDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
