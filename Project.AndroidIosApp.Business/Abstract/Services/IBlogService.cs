using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IBlogService
    {
        Task<IDataResponse<CreateBlogDto>> InsertAsync(CreateBlogDto createDto);
        Task<IDataResponse<UpdateBlogDto>> UpdateAsync(UpdateBlogDto updateDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetBlogDto>>> GetAllAsync();
        Task<IDataResponse<List<GetBlogDto>>> GetAllBySortingToCreateDateAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
    }

}
