using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.ContactDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IContactService
    {
        Task<IDataResponse<CreateContactDto>> InsertAsync(CreateContactDto createContactDto); 
        Task<IDataResponse<UpdateContactDto>> UpdateAsync(UpdateContactDto updateContactDto);
        Task<IResponse> DeleteAsync(int id); 
        Task<IDataResponse<List<GetContactDto>>> GetAllAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }
}
