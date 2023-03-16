﻿using Microsoft.AspNetCore.Http;
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
        Task<IDataResponse<CreateBlogDto>> InsertAsync(CreateBlogDto dto, IFormFile Image1, IFormFile Image2, IFormFile Image3);
        Task<IDataResponse<UpdateBlogDto>> UpdateAsync(UpdateBlogDto updateDto, IFormFile Image1, IFormFile Image2, IFormFile Image3, int id);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<List<GetBlogDto>>> GetAllAsync();
        Task<IDataResponse<List<GetBlogDto>>> GetAllBySortingToCreateDateAsync();
        Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id);
        Task<IDataResponse<GetBlogDto>> GetByIdWithProjectUserCommentAsync(int id);
    }

}
