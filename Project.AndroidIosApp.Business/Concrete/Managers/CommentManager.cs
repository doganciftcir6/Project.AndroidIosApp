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
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.ContactDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class CommentManager : ICommentService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCommentDto> _createCommentDtoValidator;
        private readonly IValidator<UpdateCommentDto> _updateCommentDtoValidator;

        public CommentManager(IUow uow, IMapper mapper, IValidator<CreateCommentDto> createCommentDtoValidator, IValidator<UpdateCommentDto> updateCommentDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createCommentDtoValidator = createCommentDtoValidator;
            _updateCommentDtoValidator = updateCommentDtoValidator;
        }

        public async Task<IResponse> DeleteCommentAsync(int id)
        {
            var value = await _uow.GetRepository<Comment>().GetByIdAsync(id);
            if (value != null)
            {
                _uow.GetRepository<Comment>().Delete(value);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, CommentMessages.DeletedComment);
            }
            else
            {
                return new Response(ResponseType.NotFound, CommentMessages.NotDeletedComment);
            }
        }

        public async Task<IDataResponse<List<GetCommentDto>>> GetAllCommentAsync()
        {
            var data = await _uow.GetRepository<Comment>().GetAllAsync();
            var dto = new List<GetCommentDto>();
            foreach (var item in data)
            {
                dto.Add(new GetCommentDto()
                {
                    Id = item.Id,
                    Content = item.Content,
                    CreateDate = item.CreateDate,
                    UpdateDate = item.UpdateDate,
                    Status = item.Status,
                });
            }
            return new DataResponse<List<GetCommentDto>>(ResponseType.Success, dto);
        }
        public async Task<IDataResponse<List<GetCommentDto>>> GetAllCommentAsyncWithUser(int id)
        {
            var query = _uow.GetRepository<Comment>().GetQuery();
            var list = await query.Where(x=> x.DeviceId == id).Include(x => x.ProjectUser).ThenInclude(x=> x.Gender).ToListAsync();
            var mappingData = _mapper.Map<List<GetCommentDto>>(list);
            return new DataResponse<List<GetCommentDto>>(ResponseType.Success, mappingData);
        }
        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<Comment>().GetByFilterAsync(x => x.Id == id);
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
                var dto = _mapper.Map<IDto>(await _uow.GetRepository<Comment>().GetByFilterAsync(x => x.Id == id));
                return new DataResponse<IDto>(ResponseType.Success, dto);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {CommentMessages.NotFoundIdComment}");
            }
        }
        //DeviceDetail sayfasını burdan çekicem çünkü tüm ilişkilerin ortak olduğun List olarak gözükmediği nokta burdaki tabloda bunu DeviceManagerda yapsaydım ilişkiler birbiini tutmazdı. Tam tersi yol izleyerek Device tablosundan değilde Comment tablosundan yaptım bu metotu ve NullReferance hatasından kurtuldum.
        public async Task<IDataResponse<GetCommentDto>> GetByIdWithProjectUserDeviceOSDeviceTypeTable(int id)
        {
            var query = _uow.GetRepository<Comment>().GetQuery();
            var value = await query.Include(x => x.ProjectUser).ThenInclude(x=> x.Gender).Include(x=> x.Device).ThenInclude(x=> x.OS).Include(x=> x.Device).ThenInclude(x=> x.DeviceType).FirstOrDefaultAsync(x => x.Id == id);
            var mappingData = _mapper.Map<GetCommentDto>(value);
            if (mappingData != null)
            {
                return new DataResponse<GetCommentDto>(ResponseType.Success, mappingData);
            }
            return new DataResponse<GetCommentDto>(ResponseType.NotFound, $"{id} {CommentMessages.NotFoundIdComment}");
        }

        public async Task<IDataResponse<CreateCommentDto>> InsertCommentAsync(CreateCommentDto createCommentDto)
        {
            var validationResult = _createCommentDtoValidator.Validate(createCommentDto);
            if (validationResult.IsValid)
            {
                var entity = new Comment()
                {
                    Content = createCommentDto.Content,
                    Status = createCommentDto.Status,
                    DeviceId = createCommentDto.DeviceId,
                    ProjectUserId = createCommentDto.ProjectUserId,
                };
                await _uow.GetRepository<Comment>().InsertAsync(entity);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateCommentDto>(ResponseType.Success, createCommentDto);
            }
            else
            {
                return new DataResponse<CreateCommentDto>(ResponseType.ValidationError, createCommentDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateCommentDto>> UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            var validationResult = _updateCommentDtoValidator.Validate(updateCommentDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<Comment>().GetByIdAsync(updateCommentDto.Id);
                if (unChangedData != null)
                {
                    _uow.GetRepository<Comment>().Update(new()
                    {
                        Id = updateCommentDto.Id,
                        Content = updateCommentDto.Content,
                        Status = updateCommentDto.Status,
                        UpdateDate = updateCommentDto.UpdateDate //BURAYI UNUTMA CONTROLLERDA DATETİME NOW ATILACAK DTO İÇİNE
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateCommentDto>(ResponseType.Success, updateCommentDto);
                }
                return new DataResponse<UpdateCommentDto>(ResponseType.NotFound, CommentMessages.NotFoundComment);
            }
            else
            {
                return new DataResponse<UpdateCommentDto>(ResponseType.ValidationError, updateCommentDto, validationResult.ConverToCustomValidationError());
            }
        }
    }
}
