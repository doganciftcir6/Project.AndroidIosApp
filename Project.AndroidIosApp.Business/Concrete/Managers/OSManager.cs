using AutoMapper;
using FluentValidation;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.OSDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class OSManager : IOSService
    {
        private readonly IUow _uow;
        private readonly IValidator<CreateOSDto> _createOSValidator;
        private readonly IValidator<UpdateOSDto> _updateOSValidator;
        private readonly IMapper _mapper;

        public OSManager(IUow uow, IValidator<CreateOSDto> createOSValidator, IValidator<UpdateOSDto> updateOSValidator, IMapper mapper)
        {
            _uow = uow;
            _createOSValidator = createOSValidator;
            _updateOSValidator = updateOSValidator;
            _mapper = mapper;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<OS>().GetByIdAsync(id);
            if (data != null)
            {
                _uow.GetRepository<OS>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(Core.Enums.ResponseType.Success, OSMessages.DeletedOS);
            }
            return new Response(Core.Enums.ResponseType.Success, OSMessages.NotDeletedOS);
        }

        public async Task<IDataResponse<List<GetOSDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<OS>().GetAllAsync();
            var dto = new List<GetOSDto>();
            foreach (var item in data)
            {
                dto.Add(new GetOSDto()
                {
                    Id = item.Id,
                    Definition = item.Definition,
                });
            }
            return new DataResponse<List<GetOSDto>>(ResponseType.Success, dto);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<OS>().GetByFilterAsync(x => x.Id == id));
            if (data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, data);
            }
            return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {OSMessages.NotFoundIdOS}");
        }

        public async Task<IDataResponse<CreateOSDto>> InsertAsync(CreateOSDto createOSDto)
        {
            var validationResult = _createOSValidator.Validate(createOSDto);
            if (validationResult.IsValid)
            {
                var data = new OS()
                {
                    Definition = createOSDto.Definition,
                };
                await _uow.GetRepository<OS>().InsertAsync(data);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateOSDto>(ResponseType.Success, createOSDto);
            }
            else
            {
                return new DataResponse<CreateOSDto>(ResponseType.ValidationError, createOSDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateOSDto>> UpdateAsync(UpdateOSDto updateOSDto)
        {
            var validatonResult = _updateOSValidator.Validate(updateOSDto);
            if (validatonResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<OS>().GetByIdAsync(updateOSDto.Id);
                if (unChangedData != null)
                {
                    _uow.GetRepository<OS>().Update(new OS()
                    {
                        Id = updateOSDto.Id,
                        Definition = updateOSDto.Definition,
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateOSDto>(ResponseType.Success, updateOSDto);
                }
                else
                {
                    return new DataResponse<UpdateOSDto>(ResponseType.NotFound, OSMessages.NotFoundOS);
                }
            }
            else
            {
                return new DataResponse<UpdateOSDto>(ResponseType.ValidationError, updateOSDto, validatonResult.ConverToCustomValidationError());
            }
        }
    }
}

