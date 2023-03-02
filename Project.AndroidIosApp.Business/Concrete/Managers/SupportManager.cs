using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Dtos.ProjectUser;
using Project.AndroidIosApp.Dtos.SupportDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class SupportManager : ISupportService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSupportDto> _createSupportDtoValidator;
        private readonly IValidator<UpdateSupportDto> _updateSupportDtoValidator;
        public SupportManager(IUow uow, IMapper mapper, IValidator<CreateSupportDto> createSupportDtoValidator, IValidator<UpdateSupportDto> updateSupportDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createSupportDtoValidator = createSupportDtoValidator;
            _updateSupportDtoValidator = updateSupportDtoValidator;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<Support>().GetByIdAsync(id);
            if(data != null) 
            {
                _uow.GetRepository<Support>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, SupportMessages.DeletedSupport);
            }
            else
            {
                return new Response(ResponseType.NotFound, SupportMessages.NotDeletedSupport);
            }
        }

        public async Task<IDataResponse<List<GetSupportDto>>> GetAllAsync()
        {
            var data = _mapper.Map<List<GetSupportDto>>(await _uow.GetRepository<Support>().GetAllAsync());
            return new DataResponse<List<GetSupportDto>>(ResponseType.Success,data);
        }

        public async Task<IDataResponse<List<GetSupportDto>>> GetAllByEmailReceiverAsync(string email)
        {
            var query = _uow.GetRepository<Support>().GetQuery();
            var data = await query.Where(x => x.Receiver == email).Include(x => x.ProjectUser).Include(x => x.Device).ToListAsync();
            var mappingData = _mapper.Map<List<GetSupportDto>>(data);
            if(mappingData.Count > 0)
            {
            return new DataResponse<List<GetSupportDto>>(ResponseType.Success, mappingData);
            }
            return new DataResponse<List<GetSupportDto>>(ResponseType.NotFound, $"{SupportMessages.NotFoundReceiverMessage}");
        }

        public async Task<IDataResponse<List<GetSupportDto>>> GetAllByEmailSenderAsync(string email)
        {
            var query = _uow.GetRepository<Support>().GetQuery();
            var data = await query.Where(x => x.Sender == email).Include(x => x.ProjectUser).Include(x => x.Device).ToListAsync();
            var mappingData = _mapper.Map<List<GetSupportDto>>(data);
            if(mappingData.Count > 0)
            {
                return new DataResponse<List<GetSupportDto>>(ResponseType.Success, mappingData);
            }
            return new DataResponse<List<GetSupportDto>>(ResponseType.NotFound, $"{SupportMessages.NotFoundSenderMessage}");
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Support>().GetByFilterAsync(x => x.Id == id));
            if(data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, data);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {SupportMessages.NotFoundIdSupport}");
            }
        }

        public async Task<IDataResponse<GetSupportDto>> GetByIdWithUserAsync(int id)
        {
            var query = _uow.GetRepository<Support>().GetQuery();
            var data = await query.Where(x => x.Id == id).Include(x => x.ProjectUser).Include(x => x.Device).FirstOrDefaultAsync();
            var mappingData = _mapper.Map<GetSupportDto>(data);
            if (data != null)
            {
                return new DataResponse<GetSupportDto>(ResponseType.Success, mappingData);
            }
            else
            {
                return new DataResponse<GetSupportDto>(ResponseType.NotFound, $"{id} {SupportMessages.NotFoundIdSupport}");
            }
        }

        public async Task<IDataResponse<CreateSupportDto>> InsertAsync(CreateSupportDto createSupportDto)
        {
            var validationRule = _createSupportDtoValidator.Validate(createSupportDto);
            if (validationRule.IsValid)
            {
                var entity = new Support()
                {
                    Title = createSupportDto.Title,
                    Content = createSupportDto.Content,
                    Sender = createSupportDto.Sender,
                    SenderName = createSupportDto.SenderName,
                    Receiver = createSupportDto.Receiver,
                    ReceiverName = createSupportDto.SenderName,
                    Date = createSupportDto.Date,
                    ProjectUserId = createSupportDto.ProjectUserId,
                    DeviceId = createSupportDto.DeviceId,
                };
                await _uow.GetRepository<Support>().InsertAsync(entity);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateSupportDto>(ResponseType.Success, createSupportDto);
            }
            else
            {
                return new DataResponse<CreateSupportDto>(ResponseType.ValidationError, createSupportDto, validationRule.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateSupportDto>> UpdateAsync(UpdateSupportDto updateSupportDto)
        {
            var validationResult = _updateSupportDtoValidator.Validate(updateSupportDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<Support>().GetByIdAsync(updateSupportDto.Id);
                if(unChangedData != null)
                {
                    _uow.GetRepository<Support>().Update(_mapper.Map<Support>(updateSupportDto), unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateSupportDto>(ResponseType.Success, updateSupportDto);
                }
                else
                {
                    return new DataResponse<UpdateSupportDto>(ResponseType.NotFound, SocialMediaMessages.NotFoundSocialMedia);
                }
            }
            else
            {
                return new DataResponse<UpdateSupportDto>(ResponseType.ValidationError, updateSupportDto, validationResult.ConverToCustomValidationError());
            }
        }
    }
}
