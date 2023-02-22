using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface ICommentService
    {
        Task<IDataResponse<CreateCommentDto>> InsertCommentAsync(CreateCommentDto createCommentDto);
        Task<IDataResponse<UpdateCommentDto>> UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task<IDataResponse<List<GetCommentDto>>> GetAllCommentAsync();
        Task<IDataResponse<List<GetCommentDto>>> GetAllCommentAsyncWithUser(int id);
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IDataResponse<GetCommentDto>> GetByIdWithProjectUserDeviceOSDeviceTypeTable(int id);
        Task<IResponse> DeleteCommentAsync(int id);

    }
}
