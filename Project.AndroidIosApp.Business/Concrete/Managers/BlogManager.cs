using AutoMapper;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Business.Helpers;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class BlogManager : IBlogService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBlogDto> _createBlogDtoValidator;
        private readonly IValidator<UpdateBlogDto> _updateBlogDtoValidator;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BlogManager(IUow uow, IMapper mapper, IValidator<CreateBlogDto> createBlogDtoValidator, IValidator<UpdateBlogDto> updateBlogDtoValidator, IHostingEnvironment hostingEnvironment)
        {
            _uow = uow;
            _mapper = mapper;
            _createBlogDtoValidator = createBlogDtoValidator;
            _updateBlogDtoValidator = updateBlogDtoValidator;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var result = await _uow.GetRepository<Blog>().GetByIdAsync(id);
            if (result != null)
            {
                _uow.GetRepository<Blog>().Delete(result);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, BlogMessages.DeletedBlog);
            }
            else
            {
                return new Response(ResponseType.NotFound, BlogMessages.NotDeletedBlog);
            }

        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Blog>().GetByFilterAsync(x => x.Id == id));
            if (data == null)
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {BlogMessages.NotFoundIdBlog}");
            }

            //var data = await _uow.GetRepository<Blog>().GetByFilterAsync(x => x.Id == id);
            //var dto = new UpdateBlogDto();
            //dto.Title = data.Title;
            //dto.Subtitle = data.Subtitle;
            //dto.Description = data.Description;
            //dto.Description2 = data.Description2;
            //dto.Description3 = data.Description3;
            //dto.Description4 = data.Description4;
            //dto.Company = data.Company;
            //dto.Image1 = data.Image1;
            //dto.Image2 = data.Image2;
            //dto.Image3 = data.Image3;
            //dto.CreateDate = data.CreateDate;
            //dto.Status = data.Status;
            return new DataResponse<IDto>(ResponseType.Success, data);
        }

        public async Task<IDataResponse<List<GetBlogDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<Blog>().GetAllAsync();
            var dto = new List<GetBlogDto>();
            foreach (var entity in data)
            {
                dto.Add(new()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Subtitle = entity.Subtitle,
                    Description = entity.Description,
                    Description2 = entity.Description2,
                    Description3 = entity.Description3,
                    Description4 = entity.Description4,
                    Company = entity.Company,
                    Image1 = entity.Image1,
                    Image2 = entity.Image2,
                    Image3 = entity.Image3,
                    CreateDate = entity.CreateDate,
                    Status = entity.Status,
                });
            }
            return new DataResponse<List<GetBlogDto>>(ResponseType.Success, dto);
        }
        public async Task<IDataResponse<List<GetBlogDto>>> GetAllBySortingToCreateDateAsync()
        {
            var data = await _uow.GetRepository<Blog>().GetAllBySorting(x => x.CreateDate);
            var dto = new List<GetBlogDto>();
            foreach (var entity in data)
            {
                dto.Add(new()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Subtitle = entity.Subtitle,
                    Description = entity.Description,
                    Description2 = entity.Description2,
                    Description3 = entity.Description3,
                    Description4 = entity.Description4,
                    Company = entity.Company,
                    Image1 = entity.Image1,
                    Image2 = entity.Image2,
                    Image3 = entity.Image3,
                    CreateDate = entity.CreateDate,
                    Status = entity.Status,
                });
            }
            return new DataResponse<List<GetBlogDto>>(ResponseType.Success, dto);
        }

        public async Task<IDataResponse<CreateBlogDto>> InsertAsync(CreateBlogDto dto, IFormFile Image1, IFormFile Image2, IFormFile Image3)
        {
            if (Image1 != null)
            {
                dto.Image1 = Image1.FileName;
            }
            var validationResult = _createBlogDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                //upload burda yapılabilir zaten modelim yok dto ile çalışıyorum.
                //upload
                var uploadClass = new BlogImageUploadAfterWwwroot(_hostingEnvironment);
                //erorr mesajlarım bu değişekende saklanacak ve en son hepsi gösterilecek ne kadar error varsa bu sayede bir kontrolde hata olunca diğerlerine bakılamma sorunundan kurtulacağım.
                var errorMessages = new List<string>();
                if (Image1 != null)
                {
                    var uploadResponse = uploadClass.RunUpload(Image1);
                    if (uploadResponse.Result.ResponseType == ResponseType.Success)
                    {
                        //data yerine message kullandım veri string olduğu için message olarak algılıyor entity olarak değil.
                        dto.Image1 = uploadResponse.Result.Meessage;
                    }
                    else
                    {
                        errorMessages.Add(uploadResponse.Result.Meessage);
                    }
                }
                if (Image2 != null)
                {
                    var uploadResponse2 = uploadClass.RunUpload(Image2);
                    if (uploadResponse2.Result.ResponseType == ResponseType.Success)
                    {
                        dto.Image2 = uploadResponse2.Result.Meessage;
                    }
                    else
                    {
                        errorMessages.Add(uploadResponse2.Result.Meessage);
                    }
                }
                if (Image3 != null)
                {
                    var uploadResponse3 = uploadClass.RunUpload(Image3);
                    if (uploadResponse3.Result.ResponseType == ResponseType.Success)
                    {
                        dto.Image3 = uploadResponse3.Result.Meessage;
                    }
                    else
                    {
                        errorMessages.Add(uploadResponse3.Result.Meessage);
                    }
                }
                if (errorMessages.Any())
                {
                    //tüm mesajları birleştirip controller'a öyle yollayalım burda hata alamamak için, ayırma işlemini controllerda yapacağız, mesajları liste olarak buraya yazarsak hata alırız.
                    string errorMessage = string.Join(Environment.NewLine, errorMessages);
                    return new DataResponse<CreateBlogDto>(ResponseType.Error, errorMessage);
                }

                var entity = new Blog();

                entity.Title = dto.Title;
                entity.Subtitle = dto.Subtitle;
                entity.Description = dto.Description;
                entity.Description2 = dto.Description2;
                entity.Description3 = dto.Description3;
                entity.Description4 = dto.Description4;
                entity.Company = dto.Company;
                entity.Image1 = dto.Image1;
                entity.Image2 = dto.Image2;
                entity.Image3 = dto.Image3;
                entity.Status = dto.Status;

                await _uow.GetRepository<Blog>().InsertAsync(entity);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateBlogDto>(ResponseType.Success, dto);
            }
            else
            {
                return new DataResponse<CreateBlogDto>(ResponseType.ValidationError, dto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateBlogDto>> UpdateAsync(UpdateBlogDto updateDto, IFormFile Image1, IFormFile Image2, IFormFile Image3, int id)
        {
            //eğer upload kısımlarına veri girilmeden güncelleme yapılırsa mevcut verileri kaybetmemek için;
            var loadedData = await _uow.GetRepository<Blog>().GetByIdAsync(id);
            if(loadedData != null)
            {
            updateDto.Image1 = Image1 != null ? Image1.FileName : loadedData.Image1;
            updateDto.Image2 = Image2 != null ? Image2.FileName : loadedData.Image2;
            updateDto.Image3 = Image3 != null ? Image3.FileName : loadedData.Image3;
            }
            else
            {
                return new DataResponse<UpdateBlogDto>(ResponseType.Error, "Kaynak veri bulunamadı");
            }

            var validationResult = _updateBlogDtoValidator.Validate(updateDto);
            if (validationResult.IsValid)
            {
                //upload burda yapılabilir zaten modelim yok dto ile çalışıyorum.
                //upload
                var uploadClass = new BlogImageUploadAfterWwwroot(_hostingEnvironment);
                //erorr mesajlarım bu değişekende saklanacak ve en son hepsi gösterilecek ne kadar error varsa bu sayede bir kontrolde hata olunca diğerlerine bakılamma sorunundan kurtulacağım.
                var errorMessages = new List<string>();
                if (Image1 != null)
                {
                    var uploadResponse = uploadClass.RunUpload(Image1);
                    if (uploadResponse.Result.ResponseType == ResponseType.Success)
                    {
                        //data yerine message kullandım veri string olduğu için message olarak algılıyor entity olarak değil.
                        updateDto.Image1 = uploadResponse.Result.Meessage;
                    }
                    else
                    {
                        errorMessages.Add(uploadResponse.Result.Meessage);
                    }
                }
                if (Image2 != null)
                {
                    var uploadResponse2 = uploadClass.RunUpload(Image2);
                    if (uploadResponse2.Result.ResponseType == ResponseType.Success)
                    {
                        updateDto.Image2 = uploadResponse2.Result.Meessage;
                    }
                    else
                    {
                        errorMessages.Add(uploadResponse2.Result.Meessage);
                    }
                }
                if (Image3 != null)
                {
                    var uploadResponse3 = uploadClass.RunUpload(Image3);
                    if (uploadResponse3.Result.ResponseType == ResponseType.Success)
                    {
                        updateDto.Image3 = uploadResponse3.Result.Meessage;
                    }
                    else
                    {
                        errorMessages.Add(uploadResponse3.Result.Meessage);
                    }
                }
                if (errorMessages.Any())
                {
                    //tüm mesajları birleştirip controller'a öyle yollayalım burda hata alamamak için, ayırma işlemini controllerda yapacağız, mesajları liste olarak buraya yazarsak hata alırız.
                    string errorMessage = string.Join(Environment.NewLine, errorMessages);
                    return new DataResponse<UpdateBlogDto>(ResponseType.Error, errorMessage);
                }

                var updatedEntity = await _uow.GetRepository<Blog>().GetByIdAsync(updateDto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Blog>().Update(new()
                    {
                        Id = updatedEntity.Id,
                        Title = updateDto.Title,
                        Subtitle = updateDto.Subtitle,
                        Description = updateDto.Description,
                        Description2 = updateDto.Description2,
                        Description3 = updateDto.Description3,
                        Description4 = updateDto.Description4,
                        Company = updateDto.Company,
                        Image1 = updateDto.Image1,
                        Image2 = updateDto.Image2,
                        Image3 = updateDto.Image3,
                        Status = updateDto.Status,
                    }, updatedEntity);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateBlogDto>(ResponseType.Success, updateDto);
                }
                return new DataResponse<UpdateBlogDto>(ResponseType.NotFound, BlogMessages.NotFoundBlog);
            }
            else
            {
                return new DataResponse<UpdateBlogDto>(ResponseType.ValidationError, updateDto, validationResult.ConverToCustomValidationError());
            }

        }

        public async Task<IDataResponse<GetBlogDto>> GetByIdWithProjectUserCommentAsync(int id)
        {
            var query = _uow.GetRepository<Blog>().GetQuery();
            var data = await query.Where(x => x.Id == id).Include(x => x.BlogComments).ThenInclude(x => x.ProjectUser).FirstOrDefaultAsync();
            var mappingData = _mapper.Map<GetBlogDto>(data);
            if (mappingData != null)
            {
                return new DataResponse<GetBlogDto>(ResponseType.Success, mappingData);
            }
            return new DataResponse<GetBlogDto>(ResponseType.NotFound, $"{id} {BlogMessages.NotFoundIdBlog}");
        }
    }
}



