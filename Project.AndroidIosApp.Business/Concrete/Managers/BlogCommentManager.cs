using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Business.ValidationRules.FluentValidation;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class BlogCommentManager : IBlogCommentService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBlogCommentDto> _createBlogCommentDto;
        private readonly IValidator<UpdateBlogCommentDto> _updateBlogCommentDto;

        public BlogCommentManager(IUow uow, IMapper mapper, IValidator<CreateBlogCommentDto> createBlogCommentDto, IValidator<UpdateBlogCommentDto> updateBlogCommentDto)
        {
            _uow = uow;
            _mapper = mapper;
            _createBlogCommentDto = createBlogCommentDto;
            _updateBlogCommentDto = updateBlogCommentDto;
        }

        public async Task<IResponse> DeleteBlogCommentAsync(int id)
        {
            var value = await _uow.GetRepository<BlogComment>().GetByIdAsync(id);
            if (value != null)
            {
                _uow.GetRepository<BlogComment>().Delete(value);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, BlogCommentMessages.DeletedBlogComment);
            }
            else
            {
                return new Response(ResponseType.NotFound, BlogCommentMessages.NotDeletedBlogComment);
            }
        }

        public async Task<IDataResponse<List<GetBlogCommentDto>>> GetAllBlogCommentAsync()
        {
            var data = await _uow.GetRepository<BlogComment>().GetAllAsync();
            var dto = new List<GetBlogCommentDto>();
            foreach (var item in data)
            {
                dto.Add(new GetBlogCommentDto()
                {
                    Id = item.Id,
                    Content = item.Content,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    Status = item.Status,
                });
            }
            return new DataResponse<List<GetBlogCommentDto>>(ResponseType.Success, dto);
        }

        public async Task<IDataResponse<List<GetBlogCommentDto>>> GetAllBlogCommentWithUserAndBlogAsync()
        {
            var query = _uow.GetRepository<BlogComment>().GetQuery();
            var data = await query.Include(x => x.ProjectUser).Include(x => x.Blog).ToListAsync();
            var mappingData = _mapper.Map<List<GetBlogCommentDto>>(data);
            return new DataResponse<List<GetBlogCommentDto>>(ResponseType.Success, mappingData);
        }

        public async Task<IDataResponse<List<GetBlogCommentDto>>> GetAllBlogCommentWithUserAsync(int id)
        {
            var query = _uow.GetRepository<BlogComment>().GetQuery();
            var data = await query.Where(x => x.BlogId == id).Include(x => x.ProjectUser).ThenInclude(x => x.Gender).ToListAsync();
            var mappingData = _mapper.Map<List<GetBlogCommentDto>>(data);
            return new DataResponse<List<GetBlogCommentDto>>(ResponseType.Success, mappingData);
        }
        
        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<BlogComment>().GetByFilterAsync(x => x.Id == id);
            if (data != null)
            {
                //var dto = new GetContactDto()
                //{
                //    Id = item.Id,
                //    Content = item.Content,
                //    CreateDate = item.CreateDate,
                //    UpdateDate = item.UpdateDate,
                //    Status = item.Status,
                //};
                var dto = _mapper.Map<IDto>(await _uow.GetRepository<BlogComment>().GetByFilterAsync(x => x.Id == id));
                return new DataResponse<IDto>(ResponseType.Success, dto);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {BlogCommentMessages.NotFoundIdBlogComment}");
            }
        }
        public async Task<IDataResponse<GetBlogCommentDto>> GetByIdWithBlogAndUserTableAsync(int id)
        {
            var query = _uow.GetRepository<BlogComment>().GetQuery();
            var data = await query.Include(x => x.Blog).Include(x => x.ProjectUser).ThenInclude(x => x.Gender).FirstOrDefaultAsync(x => x.Id == id);
            var mappingData = _mapper.Map<GetBlogCommentDto>(data);
            if (mappingData != null)
            {
                return new DataResponse<GetBlogCommentDto>(ResponseType.Success, mappingData);
            }
            return new DataResponse<GetBlogCommentDto>(ResponseType.NotFound, $"{id} {BlogCommentMessages.NotFoundIdBlogComment}");
        }

        public async Task<IDataResponse<CreateBlogCommentDto>> InsertBlogCommentAsync(CreateBlogCommentDto createBlogCommentDto)
        {
            var validationResult = _createBlogCommentDto.Validate(createBlogCommentDto);
            if (validationResult.IsValid)
            {
                var entity = new BlogComment()
                {
                    Content = createBlogCommentDto.Content,
                    Status = createBlogCommentDto.Status,
                    BlogId = createBlogCommentDto.BlogId,
                    ProjectUserId = createBlogCommentDto.ProjectUserId,
                };
                await _uow.GetRepository<BlogComment>().InsertAsync(entity);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateBlogCommentDto>(ResponseType.Success, createBlogCommentDto);
            }
            else
            {
                return new DataResponse<CreateBlogCommentDto>(ResponseType.ValidationError, createBlogCommentDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateBlogCommentDto>> UpdateBlogCommentAsync(UpdateBlogCommentDto updateBlogCommentDto)
        {
            var validationResult = _updateBlogCommentDto.Validate(updateBlogCommentDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<BlogComment>().GetByIdAsync(updateBlogCommentDto.Id);
                if (unChangedData != null)
                {
                    _uow.GetRepository<BlogComment>().Update(new()
                    {
                        Id = updateBlogCommentDto.Id,
                        Content = updateBlogCommentDto.Content,
                        Status = updateBlogCommentDto.Status,
                        UpdateDate = updateBlogCommentDto.UpdateDate, //BURAYI UNUTMA CONTROLLERDA DATETİME NOW ATILACAK DTO İÇİNE
                        ProjectUserId = updateBlogCommentDto.ProjectUserId,
                        BlogId = updateBlogCommentDto.BlogId,
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateBlogCommentDto>(ResponseType.Success, updateBlogCommentDto);
                }
                return new DataResponse<UpdateBlogCommentDto>(ResponseType.NotFound, BlogCommentMessages.NotFoundBlogComment);
            }
            else
            {
                return new DataResponse<UpdateBlogCommentDto>(ResponseType.ValidationError, updateBlogCommentDto, validationResult.ConverToCustomValidationError());
            }
        }
    }
}
