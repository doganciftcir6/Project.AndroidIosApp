using AutoMapper;
using FluentValidation;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.DeviceTypeDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class DeviceTypeManager : IDeviceTypeService
    {
        private readonly IUow _uow;
        private readonly IValidator<CreateDeviceTypeDto> _createDeviceTypeValidator;
        private readonly IValidator<UpdateDeviceTypeDto> _updateDeviceTypeValidator;
        private readonly IMapper _mapper;

        public DeviceTypeManager(IUow uow, IValidator<CreateDeviceTypeDto> createDeviceTypeValidator, IValidator<UpdateDeviceTypeDto> updateDeviceTypeValidator, IMapper mapper)
        {
            _uow = uow;
            _createDeviceTypeValidator = createDeviceTypeValidator;
            _updateDeviceTypeValidator = updateDeviceTypeValidator;
            _mapper = mapper;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<DeviceType>().GetByIdAsync(id);
            if (data != null)
            {
                _uow.GetRepository<DeviceType>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(Core.Enums.ResponseType.Success, DeviceTypeMessages.DeletedDeviceType);
            }
            return new Response(Core.Enums.ResponseType.Success, DeviceTypeMessages.NotDeletedDeviceType);
        }

        public async Task<IDataResponse<List<GetDeviceTypeDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<DeviceType>().GetAllAsync();
            var dto = new List<GetDeviceTypeDto>();
            foreach (var item in data)
            {
                dto.Add(new GetDeviceTypeDto()
                {
                    Id = item.Id,
                    Definition = item.Definition,
                });
            }
            return new DataResponse<List<GetDeviceTypeDto>>(ResponseType.Success, dto);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<DeviceType>().GetByFilterAsync(x => x.Id == id));
            if (data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, data);
            }
            return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {DeviceTypeMessages.NotFoundIdDeviceType}");
        }

        public async Task<IDataResponse<CreateDeviceTypeDto>> InsertAsync(CreateDeviceTypeDto createDeviceTypeDto)
        {
            var validationResult = _createDeviceTypeValidator.Validate(createDeviceTypeDto);
            if (validationResult.IsValid)
            {
                var data = new DeviceType()
                {
                    Definition = createDeviceTypeDto.Definition,
                };
                await _uow.GetRepository<DeviceType>().InsertAsync(data);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateDeviceTypeDto>(ResponseType.Success, createDeviceTypeDto);
            }
            else
            {
                return new DataResponse<CreateDeviceTypeDto>(ResponseType.ValidationError, createDeviceTypeDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateDeviceTypeDto>> UpdateAsync(UpdateDeviceTypeDto updateDeviceTypeDto)
        {
            var validatonResult = _updateDeviceTypeValidator.Validate(updateDeviceTypeDto);
            if (validatonResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<DeviceType>().GetByIdAsync(updateDeviceTypeDto.Id);
                if (unChangedData != null)
                {
                    _uow.GetRepository<DeviceType>().Update(new DeviceType()
                    {
                        Id = updateDeviceTypeDto.Id,
                        Definition = updateDeviceTypeDto.Definition,
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateDeviceTypeDto>(ResponseType.Success, updateDeviceTypeDto);
                }
                else
                {
                    return new DataResponse<UpdateDeviceTypeDto>(ResponseType.NotFound, DeviceTypeMessages.NotFoundDeviceType);
                }
            }
            else
            {
                return new DataResponse<UpdateDeviceTypeDto>(ResponseType.ValidationError, updateDeviceTypeDto, validatonResult.ConverToCustomValidationError());
            }
        }
    }
}
