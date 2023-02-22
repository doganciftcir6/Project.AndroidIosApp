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
using Project.AndroidIosApp.Dtos.GenderDto;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class GenderManager : IGenderService
    {
        private readonly IUow _uow;
        private readonly IValidator<CreateGenderDto> _createGenderValidator;
        private readonly IValidator<UpdateGenderDto> _updateGenderValidator;
        private readonly IMapper _mapper;

        public GenderManager(IUow uow, IValidator<CreateGenderDto> createGenderValidator, IValidator<UpdateGenderDto> updateGenderValidator, IMapper mapper)
        {
            _uow = uow;
            _createGenderValidator = createGenderValidator;
            _updateGenderValidator = updateGenderValidator;
            _mapper = mapper;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<Gender>().GetByIdAsync(id);
            if(data != null)
            {
                _uow.GetRepository<Gender>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(Core.Enums.ResponseType.Success,GenderMessages.DeletedGender);
            }
            return new Response(Core.Enums.ResponseType.Success, GenderMessages.NotDeletedGender);
        }

        public async Task<IDataResponse<List<GetGenderDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<Gender>().GetAllAsync();
            var dto = new List<GetGenderDto>();
            foreach (var item in data)
            {
                dto.Add(new GetGenderDto()
                {
                    Id = item.Id,
                    Definition = item.Definition,
                });
            }
            return new DataResponse<List<GetGenderDto>>(ResponseType.Success,dto);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            //var data = await _uow.GetRepository<Gender>().GetByFilterAsync(x => x.Id == id);
            //var dto = new GetGenderDto()
            //{
            //    Id = data.Id,
            //    Definition = data.Definition
            //};
            //return dto;
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Gender>().GetByFilterAsync(x => x.Id == id));
            if(data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, data);
            }
            return new DataResponse<IDto>(ResponseType.NotFound,$"{id} {GenderMessages.NotFoundIdGender}");
        }

        public async Task<IDataResponse<CreateGenderDto>> InsertAsync(CreateGenderDto createGenderDto)
        {
            var validationResult = _createGenderValidator.Validate(createGenderDto);
            if (validationResult.IsValid)
            {
                var data = new Gender()
                {
                    Definition = createGenderDto.Definition,
                };
                await _uow.GetRepository<Gender>().InsertAsync(data);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateGenderDto>(ResponseType.Success, createGenderDto);
            }
            else
            {
                return new DataResponse<CreateGenderDto>(ResponseType.ValidationError, createGenderDto ,validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateGenderDto>> UpdateAsync(UpdateGenderDto updateGenderDto)
        {
            var validatonResult = _updateGenderValidator.Validate(updateGenderDto);
            if (validatonResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<Gender>().GetByIdAsync(updateGenderDto.Id);
                if(unChangedData != null)
                {
                    _uow.GetRepository<Gender>().Update(new Gender()
                    {
                        Id = updateGenderDto.Id,
                        Definition = updateGenderDto.Definition,
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateGenderDto>(ResponseType.Success, updateGenderDto);
                }
                else
                {
                    return new DataResponse<UpdateGenderDto>(ResponseType.NotFound, GenderMessages.NotFoundGender);
                }
            }
            else
            {
                return new DataResponse<UpdateGenderDto>(ResponseType.ValidationError, updateGenderDto, validatonResult.ConverToCustomValidationError());
            }
        }
    }
}
