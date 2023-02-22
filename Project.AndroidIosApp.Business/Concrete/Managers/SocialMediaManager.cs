using AutoMapper;
using FluentValidation;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.SocialMediaDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class SocialMediaManager : ISocialMediaService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSocialMediaDto> _createSocialMediaValidator;
        private readonly IValidator<UpdateSocialMediaDto> _updateSocialMediaValidator;
        public SocialMediaManager(IUow uow, IMapper mapper, IValidator<CreateSocialMediaDto> createSocialMediaValidator, IValidator<UpdateSocialMediaDto> updateSocialMediaValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createSocialMediaValidator = createSocialMediaValidator;
            _updateSocialMediaValidator = updateSocialMediaValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<SocialMedia>().GetByIdAsync(id);
            if(data != null)
            {
                _uow.GetRepository<SocialMedia>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, SocialMediaMessages.DeletedSocialMedia);
            }
            else
            {
                return new Response(ResponseType.NotFound, SocialMediaMessages.NotDeletedSocialMedia);
            }
        }

        public async Task<IDataResponse<List<GetSocialMediaDto>>> GetAllAsync()
        {
            var data = _mapper.Map<List<GetSocialMediaDto>>(await _uow.GetRepository<SocialMedia>().GetAllAsync());
            return new DataResponse<List<GetSocialMediaDto>>(ResponseType.Success, data);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<SocialMedia>().GetByFilterAsync(x => x.Id == id));
            if(data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success,data);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound,$"{id} {SocialMediaMessages.NotFoundIdSocialMedia}");
            }
            
        }

        public async Task<IDataResponse<CreateSocialMediaDto>> InsertAsync(CreateSocialMediaDto createSocialMediaDto)
        {
            var validationResult = _createSocialMediaValidator.Validate(createSocialMediaDto);
            if (validationResult.IsValid)
            {
                await _uow.GetRepository<SocialMedia>().InsertAsync(_mapper.Map<SocialMedia>(createSocialMediaDto));
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateSocialMediaDto>(ResponseType.Success, createSocialMediaDto);
            }
            else
            {
                return new DataResponse<CreateSocialMediaDto>(ResponseType.ValidationError, createSocialMediaDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateSocialMediaDto>> UpdateAsync(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var validationResult = _updateSocialMediaValidator.Validate(updateSocialMediaDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<SocialMedia>().GetByIdAsync(updateSocialMediaDto.Id);
                if(unChangedData != null)
                {
                    _uow.GetRepository<SocialMedia>().Update(_mapper.Map<SocialMedia>(updateSocialMediaDto), unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateSocialMediaDto>(ResponseType.Success, updateSocialMediaDto);
                }
                else
                {
                    return new DataResponse<UpdateSocialMediaDto>(ResponseType.NotFound, SocialMediaMessages.NotFoundSocialMedia);
                }
            }
            else
            {
                return new DataResponse<UpdateSocialMediaDto>(ResponseType.ValidationError, updateSocialMediaDto, validationResult.ConverToCustomValidationError());
            }
        }
    }
}
