using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using Project.AndroidIosApp.Dtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Abstract.Services
{
    public interface IBlogCommentService
    {
        Task<IDataResponse<CreateBlogCommentDto>> InsertBlogCommentAsync(CreateBlogCommentDto createBlogCommentDto);
        Task<IDataResponse<UpdateBlogCommentDto>> UpdateBlogCommentAsync(UpdateBlogCommentDto updateBlogCommentDto);
        Task<IDataResponse<List<GetBlogCommentDto>>> GetAllBlogCommentAsync();
        Task<IDataResponse<List<GetBlogCommentDto>>> GetAllBlogCommentWithUserAndBlogAsync();
        Task<IDataResponse<List<GetBlogCommentDto>>> GetAllBlogCommentWithUserAsync(int id);
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IDataResponse<GetBlogCommentDto>> GetByIdWithBlogAndUserTableAsync(int id);
        Task<IResponse> DeleteBlogCommentAsync(int id);
    }
}
